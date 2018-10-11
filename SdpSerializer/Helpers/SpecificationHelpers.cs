using System;
using System.ComponentModel;
using System.Reflection;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Specifications;

namespace Javor.SdpSerializer.Helpers
{
    /// <summary>
    ///     Helpers for SDP specifications,
    /// </summary>
    public static class SpecificationHelpers
    {
        /// <summary>
        ///     Get description field attribute.
        /// </summary>
        /// <param name="sdpField">SDP field with valid description field.</param>
        /// <returns>Returns value of SDP description field in following format: 'sessionDescriptionField=' (eg. a=).</returns>
        /// <exception cref="CustomAttributeFormatException">Occured when description field attribute is missing,</exception>
        public static string GetDescriptionField(SdpField sdpField)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(sdpField.GetType());

            foreach (Attribute attribute in attributes)
            {
                if (attribute is DescriptionFieldAttribute)
                {
                    string sessionDescriptionField = ((DescriptionFieldAttribute) attribute).GetDescriptionField();
                    
                    return (sessionDescriptionField + "=");
                }
            }
            
            throw new CustomAttributeFormatException("Description field attribute missing.");
        }

        /// <summary>
        ///     Checks if given port is valid.
        /// </summary>
        /// <param name="port">Port number.</param>
        /// <returns>True if given port is valid port. False otherwise,</returns>
        public static bool CheckPortValidity(int port)
        {
            if (port >= 1 && port <= 65535)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Create new bandwidth from session description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>Session bandwidth object.</returns>
        /// <exception cref="SdpFormatException">Invalid format of session description field.</exception>
        public static SessionBandwidth CreateBandwidth(string sessionDescriptionField)
        {
            string fieldValue = GetFieldValue(sessionDescriptionField);
            
            try
            {            
                string[] bandwidthValues = fieldValue.Split(':');
            
                bool parsed = Int32.TryParse(bandwidthValues[1], out int value);
                if (!parsed)
                {
                    throw new SdpFormatException("Invalid format of bandwith value. Must be valid integer.");
                }

                return new SessionBandwidth(bandwidthValues[0], value)
                {
                    OriginalField = sessionDescriptionField
                };
            }
            catch (IndexOutOfRangeException)
            {
                throw new SdpFormatException("Invalid format of SDP Bandwidth field.");
            }
        }

        /// <summary>
        ///     Create new connection data from session description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>Session connection data object.</returns>
        /// <exception cref="SdpFormatException">Invalid format of session description field.</exception>
        public static SessionConnectionData CreateConnectionData(string sessionDescriptionField)
        {
            string fieldValue = GetFieldValue(sessionDescriptionField);
            
            try
            {                    
                // c=nettype addrtype connection-address
                string[] splitted = fieldValue.Split(' ');

                return new SessionConnectionData(splitted[0], splitted[1], splitted[2])
                {
                    OriginalField = sessionDescriptionField
                };
            }
            catch (IndexOutOfRangeException)
            {
                throw new SdpFormatException("Invalid format of SDP Connection Data field.");
            }
        }

        /// <summary>
        ///     Create new session media from sessdion description fields.
        /// </summary>
        /// <param name="mediaSessionDescriptionField">Session media description field.</param>
        /// <param name="mediaAttributesFields">Media attributes fields.</param>
        /// <returns>Session media object.</returns>
        /// <exception cref="SdpFormatException">Invalid format of session description field.</exception>
        public static SessionMedia CreateMedia(string mediaSessionDescriptionField, params string[] mediaAttributesFields)
        {
            // handle media description field
            string mediaFieldValue = GetFieldValue(mediaSessionDescriptionField);
            
            SessionMedia sm;
            
            try
            {   
                // m=<media> <port> <proto> <fmt>
                string[] mediaValues = mediaFieldValue.Split(' ');
                bool portParsed = Int32.TryParse(mediaValues[1], out int port); // port is expected on second position

                if (mediaValues.Length < 3 || !portParsed)
                {
                    throw new SdpFormatException();             
                }

                if (mediaValues.Length > 3)
                {
                    // create session media with fmt
                    int fmtCount = mediaValues.Length - 3;
                    string[] fmt = new string[fmtCount];
                    Array.Copy(mediaValues, 3, fmt, 0, fmtCount);
                    
                    sm = new SessionMedia(mediaValues[0], port, mediaValues[2], fmt);
                }
                else
                {
                    // create session media without fmt
                    sm = new SessionMedia(mediaValues[0], port, mediaValues[2]);
                }

                sm.OriginalField = mediaSessionDescriptionField;

            }
            catch (Exception e)
            {
                if (e is IndexOutOfRangeException || e is SdpFormatException)
                {
                    throw new SdpFormatException("Invalid format of session media field field.");
                }

                throw;
            }
            
            // handle meddia attributes
            foreach (string mediaAttribute in mediaAttributesFields)
            {
                sm.AddMediaAttribute(CreateAttribute(mediaAttribute));
            }

            return sm;
        }

        /// <summary>
        ///     Create new orgin from session description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>Session origin object.</returns>
        /// <exception cref="SdpFormatException">Invalid format of session description field.</exception>
        public static SessionOrigin CreateOrigin(string sessionDescriptionField)
        {            
            string fieldValue = GetFieldValue(sessionDescriptionField);
            
            try
            {                    
                // o=username sess-id sess-version nettype addrtype unicast-address
                string[] splitted = fieldValue.Split(' ');

                return new SessionOrigin(splitted[0], splitted[1], splitted[2], splitted[3], splitted[4], splitted[5])
                {
                    OriginalField = sessionDescriptionField
                };
            }
            catch (IndexOutOfRangeException)
            {
                throw new SdpFormatException("Invalid format of SDP Origin field.");
            }
        }

        /// <summary>
        ///     Create new session attribute from session description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>Session attribute object.</returns>
        /// <exception cref="SdpFormatException">Invalid format of session description field.</exception>
        public static SessionAttribute CreateAttribute(string sessionDescriptionField)
        {
            string fieldValue = GetFieldValue(sessionDescriptionField);
            
            string[] attributeValues = fieldValue.Split(new char[] {':'}, 2, StringSplitOptions.None);

            SessionAttribute at;
            switch (attributeValues.Length)
            {
                case 1:
                    // flag attribute
                    at = new SessionAttribute(attributeValues[0]);
                    break;
                
                case 2:
                    // value attribute
                    at = new SessionAttribute(attributeValues[0], attributeValues[1]);
                    break;
                
                default:
                    throw new SdpFormatException("Invalid format of SDP attribute.");
            }

            at.OriginalField = sessionDescriptionField;
            return at;
        }

        /// <summary>
        ///     Create new session name from session description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>Session name object.</returns>
        public static SessionName CreateSessionName(string sessionDescriptionField)
        {
            string fieldValue = GetFieldValue(sessionDescriptionField);
            
            return new SessionName(fieldValue)
            {
                OriginalField = sessionDescriptionField
            };
        }

        /// <summary>
        ///     Create new session timing from description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>Session timing object.</returns>
        /// <exception cref="SdpFormatException">Invalid format of session description field.</exception>
        public static SessionTiming CreateTiming(string sessionDescriptionField)
        {
            string fieldValue = GetFieldValue(sessionDescriptionField);
            
            try
            {                    
                string[] timingValues = fieldValue.Split(' ');

                bool startTimeParsed = Int32.TryParse(timingValues[0], out int startTime);
                bool stopTimeParsed = Int32.TryParse(timingValues[1], out int stopTime);

                if (!startTimeParsed || !stopTimeParsed)
                {
                    throw new SdpFormatException("Invalid value(s) for Timing description field.");
                }             
                
                return new SessionTiming(startTime, stopTime)
                {
                    OriginalField = sessionDescriptionField
                };
            }
            catch (IndexOutOfRangeException)
            {
                throw new SdpFormatException("Invalid format of SDP Timming field.");
            }
        }

        /// <summary>
        ///     Create new SDP version from description field.
        /// </summary>
        /// <param name="sessionDescriptionField">Session description field.</param>
        /// <returns>SdpVersion object.</returns>
        public static SdpVersion CreateVersion(string sessionDescriptionField)
        {
            string fieldValue = GetFieldValue(sessionDescriptionField);

            return new SdpVersion(fieldValue)
            {
                OriginalField = sessionDescriptionField
            };
        }
        
        private static string GetFieldValue(string sessionDescriptionField)
        {
            if (string.IsNullOrEmpty(sessionDescriptionField))
                throw new ArgumentNullException("Session description field cann't be null or empty string.");
            
            return sessionDescriptionField.Split(
                new string[] {SdpConstants.TypeDelimiter}, 2, StringSplitOptions.None)[1];
        }
    }
}