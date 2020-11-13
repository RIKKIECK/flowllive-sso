using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Security.Principal;
using System.Net;
using System.Net.Http;
using DTO;
using Newtonsoft.Json;
using BO;

namespace SSOApi.Security
{
    /// <summary>
    /// clase que busca en el header de cada peticion el token de autorizacion
    /// </summary>
    public class OAuth : AuthorizationFilterAttribute
    {
        private string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrkalgoreloco";
        private BOuser userBO = new BOuser();
        /// <summary>
        /// Decorador para las llamadas HTTP que pedirá autentificación para poder ser ejecutadas
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                try
                {
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IDateTimeProvider provider = new UtcDateTimeProvider();
                    IJwtValidator validator = new JwtValidator(serializer, provider);
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                    var json = decoder.Decode(authToken, this.secret, verify: true);
                    DTOuser usu = new DTOuser();
                    usu = JsonConvert.DeserializeObject<DTOuser>(json);
                    usu = userBO.GetUser(usu.id);
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(JsonConvert.SerializeObject(usu)), null);
                }
                catch (TokenExpiredException)
                {
                    actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
                }
                catch (SignatureVerificationException)
                {
                    actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }

        /// <summary>
        /// Genera el token para un usuario
        /// </summary>
        /// <returns></returns>
        public String CreateToken(DTOuser dto)
        {
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var secondsSinceEpoch = Math.Round((now.AddDays(1) - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                { "id", dto.id },
                { "name", dto.name},
                { "lastName", dto.lastName},
                { "subCompany", dto.subCompany},
                { "exp", secondsSinceEpoch }
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, this.secret);
            return token;
        }
    }
}