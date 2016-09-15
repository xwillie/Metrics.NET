﻿using System;
using System.Net;

namespace Metrics.Visualization
{
    public sealed class MetricsEndpoint
    {
        private readonly Func<MetricsEndpointRequest, MetricsEndpointResponse> responseFactory;

        public readonly string Endpoint;

        public MetricsEndpointResponse ProduceResponse(MetricsEndpointRequest request) => this.responseFactory(request);

        public MetricsEndpoint(string endpoint, Func<MetricsEndpointRequest, MetricsEndpointResponse> responseFactory)
        {
            if (responseFactory == null)
            {
                throw new ArgumentNullException(nameof(responseFactory));
            }

            this.Endpoint = NormalizeEndpoint(endpoint);

            this.responseFactory = responseFactory;
        }

        public bool IsMatch(string matchWith)
        {
            var normalizedMatchWith = NormalizeEndpoint(matchWith);
            return string.Equals(this.Endpoint, normalizedMatchWith, StringComparison.InvariantCultureIgnoreCase);
        }

        private static string NormalizeEndpoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint) || endpoint == "/")
            {
                throw new ArgumentException("Endpoint path cannot be empty");
            }

            if (!endpoint.StartsWith("/"))
            {
                endpoint = '/' + endpoint;
            }

            return endpoint;
        }
    }
}
