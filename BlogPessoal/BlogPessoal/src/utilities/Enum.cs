using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
    /// <summary>
    /// <para>Enum responsavel por definitir tipos de usuarios no sistema</para>
    /// <para>Data: 13/05/2022 | 12:00</para>
    /// </summary>
 [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum TypeUser
    {
        USER,
        ADMIN
    }
}
