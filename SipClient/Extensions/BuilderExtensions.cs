using System;
using System.Net;
using SipClient.Models;

namespace SipClient.Extensions
{
    public static class BuilderExtensions
    {
        public static ISipBuilder SetAllowedMethods(this ISipBuilder builder, params string[] allowedMethods)
        {
            builder.AllowedMethods = allowedMethods;
            return builder;
        }

        public static ISipBuilder SetDefaultAllowedMethods(this ISipBuilder builder)
        {
            builder.AllowedMethods = Defaults.AllowedMethods;
            return builder;
        }

        public static ISipBuilder SetRegistrarUri(this ISipBuilder builder, Uri registrarUri)
        {
            builder.RegistrarUri = registrarUri;
            return builder;
        }

        public static ISipClient BuildSipClient(this ISipBuilder builder)
        {

            DefaultSipClient toReturn = new DefaultSipClient(builder.ClientAccount);

            // TODO create builder
            return null;
        }
    }
}