using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PR_20
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PutPage : ContentPage
	{
		public PutPage ()
		{
			InitializeComponent();
		}

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var user = new PutUser();
            user.Name = "Alexander";
            user.Job = "Student";
            string json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json);
            HttpClient client = new HttpClient();
            Random random = new Random();
            int value = random.Next(1, 10);
            try
            {
                HttpResponseMessage response = await client.PutAsync($"https://reqres.in/api/users/{value}", content);
                response.EnsureSuccessStatusCode();
                var answer = await response.Content.ReadAsStringAsync();
                PutUser answer_user = JsonConvert.DeserializeObject<PutUser>(answer);
                Label1.Text = "Имя: " + answer_user.Name + "\r\n" + "Работа: " + answer_user.Job + "\r\n" + "Время обновления: " + Convert.ToString(answer_user.UpdateAt);
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

        private void OnPostButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PostPage());
        }
    }
}
