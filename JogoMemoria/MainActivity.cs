using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Graphics.Drawables;
using System.Timers;
using Android.Content;
using System.Collections.Generic;

namespace JogoMemoria
{
    [Activity(Label = "@string/app_name", Theme = "@style/NoStatusBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        Button _btnJogar, _btnMenu;
        TextView _txtAcertos, _txtErros, _txtObjetivo;
        int posicaoFruta1, posicaoFruta2;
        bool travaClique = false;
        int contadorAcertos, contadorErros = 0;
        string fruta1, fruta2 = "";
        int cartaCount = 0;
        int count = 0;
        Timer timer, timer2 = null;
        ImageButton _img1, _img2, _img3, _img4, _img5, _img6, _img7, _img8;
        Random rnd;
        int[] posicoes;
        int[] verificador;
        string[] fruits = new string[8];
        int posicao;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _img1 = FindViewById<ImageButton>(Resource.Id.img1);
            _img2 = FindViewById<ImageButton>(Resource.Id.img2);
            _img3 = FindViewById<ImageButton>(Resource.Id.img3);
            _img4 = FindViewById<ImageButton>(Resource.Id.img4);
            _img5 = FindViewById<ImageButton>(Resource.Id.img5);
            _img6 = FindViewById<ImageButton>(Resource.Id.img6);
            _img7 = FindViewById<ImageButton>(Resource.Id.img7);
            _img8 = FindViewById<ImageButton>(Resource.Id.img8);
            _txtAcertos = FindViewById<TextView>(Resource.Id.txtAcertos);
            _txtErros = FindViewById<TextView>(Resource.Id.txtErros);
            _txtObjetivo = FindViewById<TextView>(Resource.Id.txtObjetivo);
            _btnJogar = FindViewById<Button>(Resource.Id.btnJogar);
            _btnMenu = FindViewById<Button>(Resource.Id.btnMenu);
            _btnJogar.Click += _btnJogar_Click;
            _btnMenu.Click += _btnMenu_Click;
            _img1.Click += _img1_Click;
            _img2.Click += _img2_Click;
            _img3.Click += _img3_Click;
            _img4.Click += _img4_Click;
            _img5.Click += _img5_Click;
            _img6.Click += _img6_Click;
            _img7.Click += _img7_Click;
            _img8.Click += _img8_Click;
            startGame();
            _txtObjetivo.SetTextColor(Android.Graphics.Color.Green);
        }

        private void _btnMenu_Click(object sender, EventArgs e)
        {
            backToHome();
        }

        private void _btnJogar_Click(object sender, EventArgs e)
        {
            restartGame();
        }

        public void backToHome()
        {
            Intent it = new Intent(this, typeof(AtividadeTelaPrincipal));
            StartActivity(it);
        }
        public void restartGame()
        {
            contadorErros = 0;
            contadorAcertos = 0;
            setDefaultStyle();
            startGame();
        }
        public void endGame(string title, string msg)
        {
            _btnJogar.Enabled = true;
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alerta = builder.Create();
            alerta.SetCancelable(true);
            alerta.SetIcon(Android.Resource.Drawable.IcDialogInfo);
            alerta.SetTitle(title);
            alerta.SetMessage(msg);

            if (contadorAcertos == 4 && contadorErros <= 1)
            {
                alerta.SetButton("Próxima fase", (s, ev) =>
                {
                    Intent intent = new Intent(this, typeof(AtividadeFase2));
                    StartActivity(intent);
                });
                alerta.SetButton2("Jogar novamente", (s, ev) =>
                {
                    restartGame();
                });
            }else if (contadorErros > 1)
            {
                alerta.SetButton("Jogar novamente", (s, ev) =>
                {
                    restartGame();
                });
            }
            alerta.Show();
        }
        public void setFruits(int p)
        {
            if (travaClique) { return; }
            ImageButton[] img = getBtn();
            if (fruits[p] == "pear")
            {
                img[p].SetBackgroundResource(Resource.Drawable.pera_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "apple")
            {
                img[p].SetBackgroundResource(Resource.Drawable.maca_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "orange")
            {
                img[p].SetBackgroundResource(Resource.Drawable.laranja_cartoon);
                img[p].Enabled = false;
            }
            else
            {
                img[p].SetBackgroundResource(Resource.Drawable.melancia_cartoon);
                img[p].Enabled = false;
            }

            cartaCount++;
            if (cartaCount ==  1)
            {
                fruta1 = fruits[p];
                posicaoFruta1 = p;
            }else if (cartaCount == 2)
            {
                fruta2 = fruits[p];
                posicaoFruta2 = p;
                cartaCount = 0;
                if (fruta1 == fruta2)
                {
                    contadorAcertos++;
                    _txtAcertos.Text = contadorAcertos.ToString();
                }
                else
                {
                    contadorErros++;
                    _txtErros.Text = contadorErros.ToString();
                    travaClique = true;
                    img[posicaoFruta1].Enabled = true;
                    img[posicaoFruta2].Enabled = true;
                    temporizador2();
                }
                if (contadorAcertos == 4 && contadorErros <= 1)
                {
                    endGame("Parabéns, você ganhou!", "Deseja ir para próxima fase?");
                    _txtAcertos.SetTextColor(Android.Graphics.Color.Green);
                }else if (contadorErros > 1)
                {
                    endGame("Ops, você perdeu!", "Tente novamente!");
                    _txtErros.SetTextColor(Android.Graphics.Color.Red);
                }
            }
        }

        public void temporizador2()
        {
            timer2 = new Timer();
            timer2.Interval = 1000;
            timer2.Elapsed += Timer_Elapsed1;
            timer2.Start();
        }

        private void Timer_Elapsed1(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() => {

                //Toast.MakeText(this, "ok" + posicao + " ", ToastLength.Short).Show();
                ImageButton[] img = getBtn();
                img[posicaoFruta1].SetBackgroundResource(Resource.Drawable.bordaRedonda);
                img[posicaoFruta2].SetBackgroundResource(Resource.Drawable.bordaRedonda);
                travaClique = false;
                timer2.Stop();
                
            });
        }

        public void Temporizador()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            count++;
            RunOnUiThread(() => {
                if (count > 2)
                {
                    count = 0;
                    setDefaultStyle();
                    timer.Stop();

                }
            });
        }

        public ImageButton[] getBtn()
        {
            ImageButton[] camposBtn =
            {
                _img1,
                _img2,
                _img3,
                _img4,
                _img5,
                _img6,
                _img7,
                _img8
            };

            return camposBtn;
        }

        public void setDefaultStyle()
        {
            ImageButton[] btnImgCampos = getBtn();
            for (int i = 0; i < btnImgCampos.Length; i++)
            {
                btnImgCampos[i].SetBackgroundResource(Resource.Drawable.bordaRedonda);
                btnImgCampos[i].Enabled = true;
            }
        }

        public bool existe(int[] posicoes, int posicao, int tamanho)
        {
            bool valor = false;
            for (int i = 0; i < tamanho; i++)
            {
                if (posicoes[i] == posicao)
                {
                    valor = true;
                }
            }
            return valor;
        }

        public void startGame()
        {
            _btnJogar.Enabled = false;
            posicoes = new int[8];
            verificador = new int[8];

            rnd = new Random();
            //int tamanho = 0;
            for (int i = 0; i < posicoes.Length; i++)
            {

                posicao = rnd.Next(1, 9);
                while (existe(posicoes, posicao, i))
                {
                    posicao = rnd.Next(1, 9);
                }
                posicoes[i] = posicao;

                ImageButton[] btn = getBtn();

                if (posicoes[i] == 1)
                {
                    fruits[i] = "pear";
                    btn[i].SetBackgroundResource(Resource.Drawable.pera_cartoon);
                    
                }else if (posicoes[i] == 2)
                {
                    fruits[i] = "apple";
                    btn[i].SetBackgroundResource(Resource.Drawable.maca_cartoon);
                }
                else if (posicoes[i] == 3)
                {
                    fruits[i] = "orange";
                    btn[i].SetBackgroundResource(Resource.Drawable.laranja_cartoon);
                }
                else if (posicoes[i] == 4)
                {
                    fruits[i] = "watermelon";
                    btn[i].SetBackgroundResource(Resource.Drawable.melancia_cartoon);
                }
                else if (posicoes[i] == 5)
                {
                    fruits[i] = "pear";
                    btn[i].SetBackgroundResource(Resource.Drawable.pera_cartoon);
                }else if (posicoes[i] == 6)
                {
                    fruits[i] = "apple";
                    btn[i].SetBackgroundResource(Resource.Drawable.maca_cartoon);
                }
                else if (posicoes[i] == 7)
                {
                    fruits[i] = "orange";
                    btn[i].SetBackgroundResource(Resource.Drawable.laranja_cartoon);
                }
                else
                {
                    fruits[i] = "watermelon";
                    btn[i].SetBackgroundResource(Resource.Drawable.melancia_cartoon);
                }
            }
            Temporizador();
        }

        private void _img8_Click(object sender, EventArgs e)
        {
            setFruits(7);
        }

        private void _img7_Click(object sender, EventArgs e)
        {
            setFruits(6);
        }

        private void _img6_Click(object sender, EventArgs e)
        {
            setFruits(5);
        }

        private void _img5_Click(object sender, EventArgs e)
        {
            setFruits(4);
        }

        private void _img4_Click(object sender, EventArgs e)
        {
            setFruits(3);
        }

        private void _img3_Click(object sender, EventArgs e)
        {
            setFruits(2);
        }

        private void _img2_Click(object sender, EventArgs e)
        {
            setFruits(1);
        }
        private void _img1_Click(object sender, EventArgs e)
        {
            setFruits(0);
        }
    }
}