﻿using GPO_BLAZOR.Client.Class.Date;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;

namespace GPO_BLAZOR.Client.Pages
{
    public partial class AutorizationForm
    {
        //[Parameter]
        //public CookieStorageAccessor cookieStorage {  get; set; }

        [Parameter]
        public IAuthorizationDate AuthorizationInterface { get; set; }

        [Parameter]
        public EventCallback<IAuthorizationDate> AuthorizationInterfaceChanged { get; set; }

        private string message = null;
        private bool isLoading { get; set; } = true;

        async Task ButtonClicked()
        {

            Console.WriteLine("Callback0: "+ AuthorizationInterface.IsCookies+ " "+ AuthorizationInterface.GetHashCode());
            try
            {
                Console.WriteLine(AuthorizationInterface);
                await AuthorizationInterface.GetValues(ReadCookies);
                Console.WriteLine("CookeiIsRead");
                await AuthorizationInterfaceChanged.InvokeAsync(AuthorizationInterface);
                Console.WriteLine("ChangeInterface");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //await Checer();
                message = "Неверное имя пользователя или пароль";
            }

        }

        string value = "value";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            isLoading = false;
        }




        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            
            try
            {
                if (AuthorizationInterface._writer == null)
                    AuthorizationInterface = new AuthorizationDate(ReadCookies, WriteCookies);
                await AuthorizationInterface.GetValues(ReadCookies);
                await AuthorizationInterfaceChanged.InvokeAsync(AuthorizationInterface);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Checer();
            }
            finally
            {
            }

        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (AuthorizationInterface._writer==null)
            AuthorizationInterface = new AuthorizationDate(ReadCookies, WriteCookies);
        }


        protected async Task Checer()
        {
            await AuthorizationInterface.GetValues(ReadCookies);
            await AuthorizationInterfaceChanged.InvokeAsync();
        }




        protected async Task WriteCookies(string key, string value)
        {
            //await JSRuntime.InvokeAsync<string>("WriteCookie.WriteCookie", "token", value, DateTime.Now.AddMinutes(1));
            Console.WriteLine(DateTime.Now.AddMinutes(1));
            await cookieStorage.WriteCookieAsync(key, value, DateTime.Now.AddMinutes(1));
        }


        protected async Task<string> ReadCookies(string key)
        {
            var temp = await cookieStorage.ReadCookieAsync<string>(key);
            try
            {
                if (temp != null || temp != "")
                {
                    await AuthorizationInterfaceChanged.InvokeAsync(AuthorizationInterface);
                }
                return temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ("ReadCookies Error -116 string");
            }
        }

    }
}
