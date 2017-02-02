using Stimulsoft.Base.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace NetTimeZone
{
    public class TimeZoneAPI
    {
        private string GoogleTimeZoneBase_URL = "https://maps.googleapis.com/maps/api/timezone/json?";
        private string key_server;
        public TimeZoneAPI(String Key_Server)
        {
            this.key_server = Key_Server;
        }
        public DateTime GetDateTime(TimeZoneParam param)
        {
            //first create a valid url request
            this.GoogleTimeZoneBase_URL += "location=" + param.location + "&timestamp=" + param.timestamp + "&key=" + key_server;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GoogleTimeZoneBase_URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse respuesta = (HttpWebResponse)request.GetResponse();
            if (respuesta.StatusCode == HttpStatusCode.Accepted || respuesta.StatusCode == HttpStatusCode.OK || respuesta.StatusCode == HttpStatusCode.Created)
            {
                StreamReader read = new StreamReader(respuesta.GetResponseStream());
                String result = read.ReadToEnd();
                read.Close();
                respuesta.Close();
                TimeZoneResponse res_ = JsonConvert.DeserializeObject<TimeZoneResponse>(result);
                if (res_.status != "OK")
                {
                    throw new Exception("TimeZoneAPI _" + res_.status);
                }
                double res = param.timestamp + res_.dstOffset + res_.rawOffset;
                //TimeSpan ts = new TimeSpan(res);
                DateTime dt = new DateTime(1970, 1, 1).AddSeconds(res);
                return dt;
            }
            else
            {
                throw new Exception("Ocurrio un error al obtener la respuesta del servidor: " + respuesta.StatusCode);
            }
        }
        public static long DateTimeToUnixTimestamp()
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
            return unixTime;
        }
        public static long DateTimeToUnixTimestamp(DateTime dt)
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((dt.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
            return unixTime;
        }
    }
    public class TimeZoneParam
    {
        private string _location;
        private long _timestamp;
        private string _language;

        public string location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

        public long timestamp
        {
            get
            {
                return _timestamp;
            }

            set
            {
                _timestamp = value;
            }
        }

        public string language
        {
            get
            {
                return _language;
            }

            set
            {
                _language = value;
            }
        }
    }
    public class TimeZoneResponse
    {
        private long _dstOffset;
        private long _rawOffset;
        private string _status;
        private string _timeZoneId;
        private string _timeZoneName;

        public long dstOffset
        {
            get
            {
                return _dstOffset;
            }

            set
            {
                _dstOffset = value;
            }
        }

        public long rawOffset
        {
            get
            {
                return _rawOffset;
            }

            set
            {
                _rawOffset = value;
            }
        }

        public string status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public string timeZoneId
        {
            get
            {
                return _timeZoneId;
            }

            set
            {
                _timeZoneId = value;
            }
        }

        public string timeZoneName
        {
            get
            {
                return _timeZoneName;
            }

            set
            {
                _timeZoneName = value;
            }
        }
    }
}
