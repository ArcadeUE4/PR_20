using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PR_20
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            Random rnd = new Random();
            int value = rnd.Next(1, 10);
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://reqres.in/api/users/{value}");
                response.EnsureSuccessStatusCode();
                var answer = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(answer);
                var str = o.SelectToken(@"$.data");
                GetUser answer_user = JsonConvert.DeserializeObject<GetUser>(str.ToString());
                Label1.Text = "ID: " + answer_user.ID + "\r\n" + "Email: " + answer_user.Email + "\r\n" + "Имя: " + answer_user.First_Name + "\r\n" + "Фамилия: " + answer_user.Last_Name + "\r\n" + "Аватар: " + answer_user.Avatar;
            }

            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        private void OnPutButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PutPage());
        }

        private void OnPostButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PostPage());
        }
    }
}
