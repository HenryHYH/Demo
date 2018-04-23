﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HelloWeb.MessageSystem.OpenSearch.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class SearchRequest
    {
        /// <summary>
        /// Initializes a new instance of the SearchRequest class.
        /// </summary>
        public SearchRequest() { }

        /// <summary>
        /// Initializes a new instance of the SearchRequest class.
        /// </summary>
        public SearchRequest(string appName = default(string), IList<string> fetchFields = default(IList<string>), int? hits = default(int?), string kvparis = default(string), string query = default(string), int? start = default(int?))
        {
            AppName = appName;
            FetchFields = fetchFields;
            Hits = hits;
            Kvparis = kvparis;
            Query = query;
            Start = start;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "appName")]
        public string AppName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fetchFields")]
        public IList<string> FetchFields { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hits")]
        public int? Hits { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "kvparis")]
        public string Kvparis { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "start")]
        public int? Start { get; set; }

    }
}