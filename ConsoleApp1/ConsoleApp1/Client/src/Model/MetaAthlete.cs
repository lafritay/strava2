using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Model
{
    /// <summary>
    ///
    /// </summary>
    [DataContract]
    public class MetaAthlete
    {
        /// <summary>
        /// The unique identifier of the athlete
        /// </summary>
        /// <value>The unique identifier of the athlete</value>
        [DataMember(Name = "firstname", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastname", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}