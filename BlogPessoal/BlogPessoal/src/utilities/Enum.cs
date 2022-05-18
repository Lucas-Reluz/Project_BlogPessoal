using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
    /// <summary>
    /// <para>Enum responsible for defining the types of users in the system</para>
    /// <para>Data: 13/05/2022 | 12:00</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum TypeUser
    {
        USER,
        ADMIN
    }
}
