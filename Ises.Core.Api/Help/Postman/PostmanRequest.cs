﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Ises.Core.Api.Help.Postman
{
    public class PostmanRequest
    {
        public PostmanRequest()
        {
            Id = Guid.NewGuid();
            Synced = false;
            DescriptionFormat = "markdown";
            Version = 2;
            Responses = new Collection<string>();
            PathVariables = new Dictionary<string, string>();
        }
        /// <summary>
        ///     id of request
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     headers associated with the request
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        public string Headers { get; set; }

        /// <summary>
        ///     url of the request
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        ///     path variables of the request
        /// </summary>
        [JsonProperty(PropertyName = "pathVariables")]
        public Dictionary<string, string> PathVariables { get; set; }

        /// <summary>
        ///     method of request
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        /// <summary>
        ///     data to be sent with the request
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        /// <summary>
        ///     data mode of reqeust
        /// </summary>
        [JsonProperty(PropertyName = "dataMode")]
        public string DataMode { get; set; }

        /// <summary>
        ///     name of request
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     request description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     format of description
        /// </summary>
        [JsonProperty(PropertyName = "descriptionFormat")]
        public string DescriptionFormat { get; set; }

        /// <summary>
        ///     time that this request object was generated
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public long Time { get; set; }

        /// <summary>
        ///     version of the request object
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public object Version { get; set; }

        /// <summary>
        ///     request response
        /// </summary>
        [JsonProperty(PropertyName = "responses")]
        public ICollection<string> Responses { get; set; }

        /// <summary>
        ///     the id of the collection that the request object belongs to
        /// </summary>
        [JsonProperty(PropertyName = "collectionId")]
        public Guid CollectionId { get; set; }

        /// <summary>
        ///     Synching
        /// </summary>
        [JsonProperty(PropertyName = "synced")]
        public bool Synced { get; set; }
    }
}
