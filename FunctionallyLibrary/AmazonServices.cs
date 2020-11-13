using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;

namespace FunctionallyLibrary
{
    /// <summary>
    /// cliente que contendra acciones sobre amazon
    /// </summary>
    public class AmazonServices
    {
        private IAmazonS3 client { get; set; }

        public AmazonServices()
        {
            this.client = new AmazonS3Client(Amazon.RegionEndpoint.SAEast1);
        }
        /// <summary>
        /// Guarda la imagen de perfil de usuario
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public string UploadImage(string base64String, string email)
        {
            try
            {
                if (base64String.Equals("") || base64String == null)
                {
                    return "https://sso-xo.s3-sa-east-1.amazonaws.com/Company-Img/img-avatar.jpg";
                }
                else
                {
                    Random obj = new Random();
                    string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    int longitud = posibles.Length;
                    char letra;
                    int longitudnuevacadena = 6;
                    string random = "";
                    for (int i = 0; i < longitudnuevacadena; i++)
                    {
                        letra = posibles[obj.Next(longitud)];
                        random += letra.ToString();
                    }
                    string url = "https://sso-xo.s3-sa-east-1.amazonaws.com/User-Img/" + email + random + ".jpg";
                    string base64 = base64String.Substring((base64String.IndexOf(",") + 1), (base64String.Length - (base64String.IndexOf(",") + 1)));
                    byte[] bytes = Convert.FromBase64String(base64);

                    using (this.client)
                    {
                        var request = new PutObjectRequest
                        {
                            BucketName = "sso-xo",
                            CannedACL = S3CannedACL.PublicRead,
                            Key = string.Format("User-Img/{0}", email + random + ".jpg")
                        };
                        using (var ms = new MemoryStream(bytes))
                        {
                            request.InputStream = ms;
                            client.PutObject(request);
                        }
                    }
                    return url;
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {

                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    return "Check the provided AWS Credentials. For service sign up go to http://aws.amazon.com/s3";
                }
                else
                {
                    return "Error occurred. Message:'{0}' when writing an object: " + amazonS3Exception.Message;
                }
            }
        }

        public string DeleteImgInBucket(string fileName)
        {
            
            try
            {
                DeleteObjectsRequest multiObjectDeleteRequest = new DeleteObjectsRequest();
                multiObjectDeleteRequest.BucketName = "sso-xo";
                
                string algo = fileName.Substring(fileName.IndexOf(".com/User-Img/") + 14, fileName.Length - (fileName.IndexOf(".com/User-Img/") + 14));

                multiObjectDeleteRequest.AddKey("User-Img" + algo, null); // version ID is null.
                DeleteObjectsResponse response = client.DeleteObjects(multiObjectDeleteRequest);
                return "OK";
            }
            catch (DeleteObjectsException e)
            {
                return e.ToString();
            }
            
        }
    }
}
