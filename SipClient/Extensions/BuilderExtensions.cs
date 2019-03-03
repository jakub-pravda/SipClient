using System;
using System.Net;
using Javor.SipSerializer.Schemes;
using SipClient.Models;

namespace SipClient.Extensions
{
    public static class BuilderExtensions
    {
        public static ISipBuilder SetUser(this ISipBuilder builder, string user)
        {
            builder.User = user;
            return builder;
        }

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

        public static ISipBuilder SetRegistrarUri(this ISipBuilder builder, string registrarUri)
        {
            builder.RegistrarUri = new SipUri(registrarUri);
            return builder;
        }

        public static ISipBuilder SetRegistrarUri(this ISipBuilder builder, SipUri registrarUri)
        {
            builder.RegistrarUri = registrarUri;
            return builder;
        }

        public static ISipBuilder SetRegistrarUri(this ISipBuilder builder, byte[] registrarIpAddress)
        {
            builder.RegistrarUri = new SipUri(new IPAddress(registrarIpAddress).ToString(), builder.User);
            return builder;
        }

        public static ISipBuilder SetRegistrarPort(this ISipBuilder builder, int registrarPort)
        {
            if (builder.RegistrarUri == null) throw new ArgumentNullException("Registrar sip uri must be defined before registrar port setting.");

            builder.RegistrarUri.Port = registrarPort;
            return builder;
        }

        public static ISipBuilder SetClientUri(this ISipBuilder builder, byte[] clientIpAddress, int port)
        {
            builder.ClientUri = new SipUri(new IPAddress(clientIpAddress).ToString(), port, builder.User);
            return builder;
        }

        public static ISipClient UseDefaultSipClient(this ISipBuilder builder)
        {
            SipClientAccount account = new SipClientAccount()
            {
                RegistrarUri = builder.RegistrarUri,
                User = builder.User
            };

            DefaultSipClient sipClient = new DefaultSipClient(account);
            sipClient.TransactionLayer.StartListening(builder.ClientUri.Host, builder.ClientUri.Port);

            return sipClient;

            // TODO this method should create sip builder and save it to some temp variable... sip client definition should be handled by some final builder method
        }

        public static ISipClient UseSipClient<T>(this ISipBuilder builder)
            where T : ISipClient
        {
            // TODO create builder
            throw new NotImplementedException();
        }
    }
}