using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsumoApi.models;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;

namespace ConsumoApi
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private const string BaseUrl = "https://www.freetogame.com";

        public async Task<T> GetAsync<T>(string endpoint)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                // Realizar la solicitud GET al servidor
                HttpResponseMessage response = await client.GetAsync(endpoint);

                // Verificar si la solicitud fue exitosa (código de estado 200)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar la respuesta JSON
                    string json = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
                else
                {
                    // Manejar errores
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {


            try
            {
                //ResponseBase results = await GetAsync<ResponseBase>("https://www.freetogame.com/api/games");
                // Procesar el resultado

                List<ResponseBase> results = await GetAsync<List<ResponseBase>>("https://www.freetogame.com/api/games");

                var gameList = results.Select(game => new
                {
                    Title = game.title,
                    Thumbnail = game.thumbnail,
                    Publisher = game.publisher,
                    ReleaseDate = game.release_date
                }).ToList();

                //ListViewDemo.ItemsSource = gameList;
                CollectionViewDemo.ItemsSource = gameList;



            }
            catch (Exception ex)
            {
                throw ex;
                // Manejar errores
            }
        }
    }
}