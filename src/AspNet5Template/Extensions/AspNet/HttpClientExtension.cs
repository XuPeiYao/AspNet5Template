using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public static class HttpClientExtension{
        #region Get
        public static async Task<JToken> GetJsonAsync(this HttpClient Obj, string requestUri) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await Obj.GetStringAsync(requestUri)
                )
            );
        }

        public static async Task<JToken> GetJsonAsync(this HttpClient Obj, Uri requestUri) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await Obj.GetStringAsync(requestUri)
                )
            );
        }
        #endregion

        #region Post
        public static async Task<JToken> PostJsonAsync(this HttpClient Obj, string requestUri, HttpContent content, CancellationToken cancellationToken) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PostAsync(requestUri, content, cancellationToken)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PostJsonAsync(this HttpClient Obj, Uri requestUri, HttpContent content, CancellationToken cancellationToken) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PostAsync(requestUri, content, cancellationToken)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PostJsonAsync(this HttpClient Obj, string requestUri, HttpContent content) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PostAsync(requestUri, content)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PostJsonAsync(this HttpClient Obj, Uri requestUri, HttpContent content) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PostAsync(requestUri, content)).Content.ReadAsStringAsync()
                )
            );
        }
        #endregion

        #region Delete
        public static async Task<JToken> DeleteJsonAsync(this HttpClient Obj, string requestUri, CancellationToken cancellationToken) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.DeleteAsync(requestUri, cancellationToken)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PostJsonAsync(this HttpClient Obj, Uri requestUri, CancellationToken cancellationToken) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.DeleteAsync(requestUri, cancellationToken)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> DeleteJsonAsync(this HttpClient Obj, string requestUri) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.DeleteAsync(requestUri)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PostJsonAsync(this HttpClient Obj, Uri requestUri) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.DeleteAsync(requestUri)).Content.ReadAsStringAsync()
                )
            );
        }
        #endregion

        #region Put
        public static async Task<JToken> PutJsonAsync(this HttpClient Obj, string requestUri, HttpContent content, CancellationToken cancellationToken) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PutAsync(requestUri, content, cancellationToken)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PutJsonAsync(this HttpClient Obj, Uri requestUri, HttpContent content, CancellationToken cancellationToken) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PutAsync(requestUri, content, cancellationToken)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PutJsonAsync(this HttpClient Obj, string requestUri, HttpContent content) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PutAsync(requestUri, content)).Content.ReadAsStringAsync()
                )
            );
        }

        public static async Task<JToken> PutJsonAsync(this HttpClient Obj, Uri requestUri, HttpContent content) {
            return await Task.FromResult<JToken>(
                JContainer.Parse(
                    await (await Obj.PutAsync(requestUri, content)).Content.ReadAsStringAsync()
                )
            );
        }
        #endregion
    }
}
