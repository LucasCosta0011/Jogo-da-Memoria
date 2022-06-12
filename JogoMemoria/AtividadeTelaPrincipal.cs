using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace JogoMemoria
{
    [Activity(Label = "AtividadeTelaPrincipal", Theme = "@style/NoStatusBar", MainLauncher = true)]
    public class AtividadeTelaPrincipal : Activity
    {
        Button _btnJogar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tela_principal);
            // Create your application here
            _btnJogar = FindViewById<Button>(Resource.Id.btnJogar);
            _btnJogar.Click += _btnJogar_Click;
        }

        private void _btnJogar_Click(object sender, EventArgs e)
        {
            Intent it = new Intent(this, typeof(MainActivity));
            StartActivity(it);
        }
    }
}