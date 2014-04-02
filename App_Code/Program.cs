using System;
using System.Net;
using System.Text;
using System.Xml.XPath;

namespace GourmetGuide
{
    public class Program
    {
        public static string[] getCoord(string address)
        {
            //string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&sensor=false";
            string[] coords = new string[2];
            //string addr = "3800 SW 34th Street,Gainesville,FL";
            StringBuilder url = new StringBuilder();
            url.Append("https://maps.googleapis.com/maps/api/geocode/xml?")
                .Append("address=").Append(address)
                .Append("&sensor=").Append("false")
                .Append("&key=").Append(ProjectSettings.googleAPIKey);
            //string url = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&sensor=false&key=AIzaSyCWi-4-hKzKzFarCzFWVYkmM5Cdr1umrF8";
            System.Diagnostics.Debug.WriteLine(url.ToString());
            WebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.ToString());
                request.Method = "GET";
                response = request.GetResponse();
                if (response != null)
                {
                    XPathDocument document = new XPathDocument(response.GetResponseStream());
                    XPathNavigator navigator = document.CreateNavigator();

                    // get response status
                    XPathNodeIterator statusIterator = navigator.Select("/GeocodeResponse/status");
                    while (statusIterator.MoveNext())
                    {
                        if (statusIterator.Current.Value != "OK")
                        {
                            Console.WriteLine("Error: response status = '" + statusIterator.Current.Value + "'");
                            return null;
                        }
                    }

                    // get results
                    XPathNodeIterator resultIterator = navigator.Select("/GeocodeResponse/result");
                    while (resultIterator.MoveNext())
                    {
                        XPathNodeIterator geometryIterator = resultIterator.Current.Select("geometry");
                        while (geometryIterator.MoveNext())
                        {
                            Console.WriteLine(" geometry: ");

                            XPathNodeIterator locationIterator = geometryIterator.Current.Select("location");
                            while (locationIterator.MoveNext())
                            {
                                Console.WriteLine("     location: ");

                                XPathNodeIterator latIterator = locationIterator.Current.Select("lat");
                                while (latIterator.MoveNext())
                                {
                                    coords[0] = latIterator.Current.Value;
                                    Console.WriteLine("         lat: " + latIterator.Current.Value);
                                }

                                XPathNodeIterator lngIterator = locationIterator.Current.Select("lng");
                                while (lngIterator.MoveNext())
                                {
                                    coords[1] = latIterator.Current.Value;
                                    Console.WriteLine("         lng: " + lngIterator.Current.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                Console.WriteLine("Clean up");
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            Console.WriteLine("Done.");
            //Console.ReadLine();
            System.Diagnostics.Debug.WriteLine("Returning " + coords[0] + "*******" + coords[1]);
            return coords;
        }
    }
}