using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PR_20
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        public PostPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var user = new PostUser();
            user.Name = "Alexander";
            user.Job = "Student";
            string json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json);
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PutAsync("https://reqres.in/api/users", content);
                response.EnsureSuccessStatusCode();
                var answer = await response.Content.ReadAsStringAsync();
                PostUser answer_user = JsonConvert.DeserializeObject<PostUser>(answer);
                Label1.Text = "Имя: " + answer_user.Name + "\r\n" + "Работа: " + answer_user.Job + "\r\n" + "ID: " + Convert.ToString(answer_user.ID) + "\r\n" + "Время обновления: " + Convert.ToString(answer_user.UpdateAt);
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        private void OnGetButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        private void OnPutButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PutPage());
        }
    }

}
