using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WIATest;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Timers;
using Oracle.DataAccess.Client;
using System.Drawing.Drawing2D;
using System.Configuration;

namespace Scannerapplication
{
    public partial class Form1 : Form
    {
        Image imagenReal;
        Scannerapplication.WSimage.WSimage wsimg = new Scannerapplication.WSimage.WSimage();
        private string nombreImagen;
        private int random; // es para generar un numero aleatorio cuando son varias imagenes por reservacion
        private int tipoMov; // tipo de movimiento 1. escaneo y guardado 2. buscar reserva
        public static string apellido; // informacion que se va a pasar al otro form
        string numPasaporte;
        string codPais;
        bool datok = false;
        int edad = 0;
        string carpeta = "", identificadorDoc = "", inputHotel = "", testImagePath = "", nombre = "", fecha_nac = "";
        public Thread th1;
        const int kSplashUpdateInterval_ms = 1;
        private int randomID, cuentaPasa;
        private Control _threadHelperControl;
        public MMM.Readers.FullPage.ReaderSettings puSettings;
        public MMM.Readers.FullPage.Box puDocPosition;
        private MMM.Readers.FullPage.ReaderState prPreviousState = MMM.Readers.FullPage.ReaderState.READER_ENABLED;


        public Form1(string[] args)
        {
            InitializeComponent();

            nombreImagen = args[0]; // el nombre como identificador de la/s imagen/es hotel + año + numero de reserva
                                    // Initialize helper control
            testImagePath = @"C:\ScannerOCR\" + nombreImagen + Program.Sesion + ".tif";
            _threadHelperControl = new Control();
            _threadHelperControl.CreateControl();
            Program.Reserva = args[1]; // numero de reserva hotelera
            Program.Sesion = args[2]; // sesion del sistema oracle forms
            tipoMov = Convert.ToInt16(args[3]);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (tipoMov == 1)
            {
                llenarFreserno();
            }
            else
            {
                dgvHuespedes.Visible = false;
                label1.Visible = false;
            }
            //WinAPI.NoSiempreEncima(this.Handle.ToInt32());
            msgsplash.Text = "Iniciando Escaner, por favor espere....";        
            timer1.Start();
            
        }
        private void llenarFreserno()
        {
            DateTime Hoy = DateTime.Today;
            DateTime ayer = Hoy.AddDays(-1);
            string fecha_ayer = ayer.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            string fecha_actual = Hoy.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            UConnection DB2 = new UConnection(Properties.Settings.Default.ipBD, Properties.Settings.Default.serverBD, Properties.Settings.Default.usuarioBD, Properties.Settings.Default.passBD);
            string sql2 = "select a.VN_SECUENCIA, a.VN_APELLIDO, a.VN_NOMBRE, a.VN_PASAPORTE " +
                            "from freserno a inner join freserva b on a.vn_reserva = b.rv_reserva " +
                            "where a.vn_reserva = '" + Program.Reserva + "' and b.rv_llegada between '" + fecha_ayer + "' and '" + fecha_actual + "'";

            try
            {
                var dt = new DataTable();

                if (DB2.EjecutaSQL(sql2))
                {
                    if (DB2.ora_DataReader.HasRows)
                    {
                        dt.Load(DB2.ora_DataReader);
                        dgvHuespedes.DataSource = dt;
                    }
                    else
                    {
                        dgvHuespedes.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO FUE POSIBLE OBTENER RESERVACIONES\n\n" + ex.Message);
            }
            finally
            {
                DB2.Dispose();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Make sure the splash screen is closed

            try
            {
                MMM.Readers.FullPage.Reader.Shutdown();
            }
            catch (System.DllNotFoundException except)
            {
                // only happens in the case where you started without MMMReaderHighLevelAPI.dll in the search path
            }
            base.OnClosing(e);
        }
        void DataCallbackThreadHelper(MMM.Readers.FullPage.DataType aDataType, object aData)
        {
            if (_threadHelperControl.InvokeRequired)
            {
                _threadHelperControl.Invoke(
                    new MMM.Readers.FullPage.DataDelegate(DataCallback),
                    new object[] { aDataType, aData }
                );
            }
            else
            {
                DataCallback(aDataType, aData);
            }
        }
        void DataCallback(MMM.Readers.FullPage.DataType aDataType, object aData)
        {
            pic_scan.Focus();
            bool ok = false;
            try
            {


                if (aData != null)
                {

                    switch (aDataType)
                    {
                        case MMM.Readers.FullPage.DataType.CD_CODELINE_DATA:
                            {

                                MMM.Readers.CodelineData codeline = (MMM.Readers.CodelineData)aData;
                                if (codeline.Surname != "")
                                {
                                    identificadorDoc = codeline.DocType.Substring(0, 1);
                                    datok = true;
                                    apellido = codeline.Surname;
                                    if (apellido.Contains(" "))
                                    {
                                        int endIndexDos = apellido.IndexOf(" ", 0);
                                        apellido = apellido.Substring(0, endIndexDos);
                                    }
                                    nombre = codeline.Forename;
                                    codPais = codeline.Nationality;
                                    numPasaporte = codeline.DocNumber;

                                    // obtengo el año actual
                                    DateTime Hoy2 = DateTime.Today;
                                    string anioActual = Hoy2.ToString("yy");
                                    int anioA = Convert.ToInt16(anioActual);
                                    DateTime dt = new DateTime(codeline.DateOfBirth.Year, codeline.DateOfBirth.Month, codeline.DateOfBirth.Day);
                                    fecha_nac = dt.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                                    edad = DateTime.Today.AddTicks(-dt.Ticks).Year - 1;
                                    if (codeline.DateOfBirth.Year > anioA)
                                    {
                                        edad = edad - 1900;
                                    }
                                    else
                                    {
                                        edad = edad + 2000;
                                    }
                                    // fin fecha nacimiento                 
                                }

                                break;
                            }

                        case MMM.Readers.FullPage.DataType.CD_IMAGEVIS:
                            {

                                pic_scan.Image = aData as Bitmap;
                                imagenReal = aData as Bitmap;
                                // reduzco la imagen en 150 ppp para guardarla
                                imagenReal = ScaleByPercent(imagenReal, 50);
                                if (datok && tipoMov != 1) { busca_reserva(); datok = false; }
                                else if (datok && tipoMov == 1) { guarda_datos(); datok = false; }
                                else if (!datok)
                                {
                                    // WinAPI.SiempreEncima(this.Handle.ToInt32());
                                    //WinAPI.NoSiempreEncima(this.Handle.ToInt32());
                                    if (MessageBox.Show("DESEA GUARDAR COMO IDENTIFICACIÓN U OTRO TIPO LA IMAGEN?", "!ALERTA¡", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        int filasI = 0;
                                        foreach (DataGridViewRow row in dgvHuespedes.Rows)
                                        {
                                            if (ToStringNullSafe(row.Cells["ESCANEAR"].Value) == "Y")
                                            {
                                                // obtengo la secuencia seleccionada en el grid
                                                string secuencia = row.Cells[1].Value.ToString();
                                                // marco el registro en freserno con "ID" para que no sobreescriba la información
                                                UConnection DB2 = new UConnection(Properties.Settings.Default.ipBD, Properties.Settings.Default.serverBD, Properties.Settings.Default.usuarioBD, Properties.Settings.Default.passBD);
                                                string comandoSql3 = string.Format("update freserno set vn_pasaporte = 'ID' where vn_reserva = '{0}' and vn_secuencia = {1}", Program.Reserva, secuencia);
                                                ok = DB2.EjecutaSQL(comandoSql3, ref filasI);
                                            }
                                        }
                                        // si no inserto el identificador, quiere decir que ya existe una imagen
                                        if (filasI == 1)
                                        {
                                            // creo un numero aleatorio para que no se repita
                                            Random randomID = new Random();
                                            int randomNumberID = randomID.Next(0, 100);
                                            // Obtengo la ruta para la imagen final y la guardo
                                            var image_name = nombreImagen + Program.Reserva + "ID" + Convert.ToString(randomNumberID) + ".jpg";
                                            byte[] imgb = ImageToByte(imagenReal);
                                            wsimg.Img_save(carpeta, imgb, image_name);
                                            MessageBox.Show("SE GUARDÓ LA IMAGEN SATISFACTORIAMENTE.", "¡ÉXITO!");
                                        }
                                        else
                                        {
                                            guarda_imagen();
                                        }
                                    }

                                }
                              
                                    msgsplash.Text = "Coloque el siguiente documento o cierre para continuar con el Check-in...";
                               
                                break;
                            }

                            /* case MMM.Readers.FullPage.DataType.CD_IMAGEUV:
                                 {
                                     uvImage.Image = aData as Bitmap;
                                     break;
                                 }*/


                    }


                }


            }
            catch (Exception e)
            {
                MessageBox.Show(
                    e.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        void EventCallbackThreadHelper(MMM.Readers.FullPage.EventCode aEventType)
        {
            if (_threadHelperControl.InvokeRequired)
            {
                _threadHelperControl.Invoke(
                    new MMM.Readers.FullPage.EventDelegate(EventCallback),
                    new object[] { aEventType }
                );
            }
            else
            {
                EventCallback(aEventType);
            }
        }
        void EventCallback(MMM.Readers.FullPage.EventCode aEventType)
        {
            try
            {


                switch (aEventType)
                {
                    case MMM.Readers.FullPage.EventCode.SETTINGS_INITIALISED:
                        {
                            // You may wish to change the settings immediately after they have 
                            // been loaded - for example, to turn off options that you do not 
                            // want.
                            MMM.Readers.FullPage.ReaderSettings settings;
                            MMM.Readers.ErrorCode errorCode = MMM.Readers.FullPage.Reader.GetSettings(
                                out settings
                            );

                            if (errorCode == MMM.Readers.ErrorCode.NO_ERROR_OCCURRED)
                            {
                                /* if (settings.puCameraSettings.puSplitImage == false)
                                     this.tabControl.Controls.Remove(this.ImagesRearTab);*/

                                settings.puDataToSend.send |=
                                    MMM.Readers.FullPage.DataSendSet.Flags.DOCMARKERS;
                                settings.puDataToSend.special =
                                    MMM.Readers.FullPage.DataSendSet.Flags.VISIBLEIMAGE;
                            }
                            else
                            {
                                MessageBox.Show(
                                    "GetSettings failure, check for Settings " +
                                    "structure mis-match. Error: " +
                                    errorCode.ToString(),
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
                            }
                            break;
                        }
                    case MMM.Readers.FullPage.EventCode.START_OF_DOCUMENT_DATA:
                        {
                            msgsplash.Text = "Leyendo información...";
                            break;
                        }                  

                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void InitialiseTimer(object sender, System.EventArgs e)
        {
            timer1.Stop();

            try
            {
                MMM.Readers.FullPage.Reader.EnableLogging(
                    true,
                    1,
                    -1,
                    "HLNonBlockingExample.Net.log"
                );

                //UpdateState(MMM.Readers.FullPage.ReaderState.READER_NOT_INITIALISED);
                // prDocStartTime = System.DateTime.UtcNow;

                // Thread helper delegates are used to avoid thread-safety issues, particularly 
                // with .NET framework 2.0				
                MMM.Readers.ErrorCode lResult = MMM.Readers.ErrorCode.NO_ERROR_OCCURRED;

                Microsoft.Win32.SystemEvents.PowerModeChanged += new Microsoft.Win32.PowerModeChangedEventHandler(OnPowerModeChanged);

                lResult = MMM.Readers.FullPage.Reader.Initialise(
                    new MMM.Readers.FullPage.DataDelegate(DataCallbackThreadHelper),
                    new MMM.Readers.FullPage.EventDelegate(EventCallbackThreadHelper),
                    new MMM.Readers.ErrorDelegate(ErrorCallbackThreadHelper),
                   null,
                    true,
                    false
                );

                if (lResult != MMM.Readers.ErrorCode.NO_ERROR_OCCURRED)
                {
                    MessageBox.Show(
                        "Initialise failed - " + lResult.ToString(),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                        );
                }
                Panel1.GradientBottomLeft = Color.ForestGreen;
                Panel1.GradientBottomRight = Color.ForestGreen;
                Panel1.GradientTopLeft = Color.LimeGreen;
                Panel1.GradientTopRight = Color.LimeGreen;
                msgsplash.Text = "Ya puede colocar el pasaporte a escanear.";
                //MessageBox.Show(Properties.Settings.Default.ipBD + "--" + Properties.Settings.Default.serverBD + "--" + Properties.Settings.Default.usuarioBD+"--"+ Properties.Settings.Default.passBD);
                /* WinAPI.NoSiempreEncima(this.Handle.ToInt32());
                 msgsplash = "Ya puede colocar el pasaporte a escanear.";
                 Thread splashThread = new Thread(new ThreadStart(StartSplash));
                 splashThread.Start();*/
                //MMM.Readers.FullPage.Reader.SetWarningCallback(new MMM.Readers.WarningDelegate(WarningCallbackThreadHelper));


                //			//Example of how to set the signrequest callback
                //			MMM.Readers.FullPage.Reader.SetSignRequestCallback(
                //				SignRequestCallbackThreadHelper
                //			);
            }
            catch (System.DllNotFoundException except)
            {
                MessageBox.Show(
                    except.Message +
                    "\nEnsure the \"working directory\" of the application is set to the " +
                    "3M Page Reader\\bin folder. When run within the IDE, set this through " +
                    "Properties -> Configuration Properties -> Debugging"
                );
            }
        }
        private void busca_reserva()
        {
            // consulto la fecha de 
            DateTime Hoy = DateTime.Today;
            DateTime ayer = Hoy.AddDays(-1);
            string fecha_actual = Hoy.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            string fecha_ayer = ayer.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            UConnection DB2 = new UConnection(Properties.Settings.Default.ipBD, Properties.Settings.Default.serverBD, Properties.Settings.Default.usuarioBD, Properties.Settings.Default.passBD);
            string sql2 = "select a.vn_reserva, a.vn_apellido, a.vn_nombre, b.rv_agencia, b.rv_voucher, (b.rv_adulto+b.rv_menor+b.rv_junior+b.rv_bebe) PAX " +
                            "from freserno a inner join freserva b on a.vn_reserva = b.rv_reserva " +
                            "where a.vn_secuencia = 1 and a.vn_apellido like '%" + apellido + "%' and b.rv_llegada between '" + fecha_ayer + "' and '" + fecha_actual + "' " +
                            "and b.rv_status = 'R'";

            try
            {
                var dt = new DataTable();

                if (DB2.EjecutaSQL(sql2))
                {
                    if (DB2.ora_DataReader.HasRows)
                    {

                        FrmReservaciones frm = new FrmReservaciones();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();

                    }
                    else
                    {

                        apellido = Microsoft.VisualBasic.Interaction.InputBox(@"LA CONSULTA NO DEVOLVIÓ NINGUNA RESERVACIÓN CON ESE APELLIDO., " +
                            "CAMBIELO Y PONGA ACEPTAR\n\n" + apellido, "APELLIDO A BUSCAR", apellido);
                        // si el apellido regresa vacío, no abro el grid de reservaciones
                        if (apellido != "")
                        {
                            FrmReservaciones frm = new FrmReservaciones();
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIÓ UN ERROR EN LA CONSULTA A LA BASE DE DATOS\n\n" + ex.Message, "¡ERROR!");
            }
            finally
            {
                DB2.Dispose();
            }

            // si se encontró el numero de reservacion
            if (FrmReservaciones.reservacion != null)
            {               
                int filas = 0;
                bool ok;
                UConnection DB = new UConnection(Properties.Settings.Default.ipBD, Properties.Settings.Default.serverBD, Properties.Settings.Default.usuarioBD, Properties.Settings.Default.passBD);
                string comandoSql3 = string.Format("update freserva set rv_pais = '{0}' where rv_reserva = '{1}' and rv_pais is null", codPais, FrmReservaciones.reservacion);
                ok = DB.EjecutaSQL(comandoSql3, ref filas);
                // inserto el numero de pasaporte en freserno
                string comandoSql2 = string.Format("update freserno set vn_pasaporte = '{0}', vn_edad = {1} where vn_reserva = '{2}' and vn_secuencia = 1",
                    numPasaporte, edad, FrmReservaciones.reservacion);
                ok = DB.EjecutaSQL(comandoSql2, ref filas);
                // inserto el mapeo de la sesion de oracle con el numero de reservacion
                string comandoSql = string.Format("insert into SCPASMAP (RESERVA,SESION) values ('{0}','{1}')",
                FrmReservaciones.reservacion, Program.Sesion);
                ok = DB.EjecutaSQL(comandoSql, ref filas);


                if (filas > 0)
                {
                    FileInfo file = new FileInfo(testImagePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    // consulto la fecha de llegada de la reserva
                    string comandoSql4 = string.Format("select a.RV_LLEGADA, b.HO_SIGLAS " +
                        "from freserva a inner join frhoteles b on a.RV_HOTEL_RENTA = b.HO_HOTEL where rv_reserva = '{0}'", FrmReservaciones.reservacion);
                    if (DB.EjecutaSQL(comandoSql4))
                    {
                        if (DB.ora_DataReader.HasRows)
                        {
                            while (DB.ora_DataReader.Read())
                            {
                                if (!DBNull.Value.Equals(DB.ora_DataReader["RV_LLEGADA"]))
                                {
                                    DateTime fec = Convert.ToDateTime(DB.ora_DataReader["RV_LLEGADA"]);
                                    string anio = fec.ToString("yyyy");
                                    string mes = fec.ToString("MM");
                                    string dia = fec.ToString("dd");
                                    inputHotel = Convert.ToString(DB.ora_DataReader["HO_SIGLAS"]);
                                    carpeta = Properties.Settings.Default.serverImagenes + inputHotel + "\\" + anio + "\\" + mes + "\\" + dia + "\\";
                                }
                            }
                        }
                    }
                    // si no existe el directorio, lo creo
                   /* if (!Directory.Exists(carpeta))
                    {
                        System.IO.Directory.CreateDirectory(carpeta);
                    }*/

                    var image_name = inputHotel + nombreImagen + FrmReservaciones.reservacion + identificadorDoc + "1" + ".jpg";
                    byte[] imgb = ImageToByte(imagenReal);
                    wsimg.Img_save(carpeta, imgb, image_name);
                }
                // cierro la conexión
                DB.Dispose();

            }
            else
            {
                // elimino el archivo ya guardado
                FileInfo file = new FileInfo(testImagePath);
                if (file.Exists)
                {
                    file.Delete();
                }
                pic_scan.Image = null;
            }
        }
        private void OnPowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case Microsoft.Win32.PowerModes.Resume:
                    // delay before starting up as the USB subsystem seems to take a while to startup.
                    // If you don't delay, things will recover via the error recovery system, but you'll
                    // get some "access denied" errors from USB devices until it is fully started.
                    System.Threading.Thread.Sleep(5000);
                    MMM.Readers.FullPage.Reader.SetState(prPreviousState, true);
                    //UpdateState(prPreviousState);
                    break;

                case Microsoft.Win32.PowerModes.Suspend:
                    {
                        // signal that we want to change state
                        MMM.Readers.FullPage.ReaderState lCurrentState
                             = MMM.Readers.FullPage.Reader.GetState();
                        prPreviousState = lCurrentState;

                        if ((lCurrentState != MMM.Readers.FullPage.ReaderState.READER_NOT_INITIALISED) &&
                            (lCurrentState != MMM.Readers.FullPage.ReaderState.READER_ERRORED) &&
                            (lCurrentState != MMM.Readers.FullPage.ReaderState.READER_TERMINATED) &&
                            (lCurrentState != MMM.Readers.FullPage.ReaderState.READER_SUSPENDED))
                        {
                            MMM.Readers.FullPage.Reader.SetState(
                                MMM.Readers.FullPage.ReaderState.READER_SUSPENDED,
                                true
                                );

                            // Wait for the state change to be applied
                            do
                            {
                                System.Threading.Thread.Sleep(10);
                                lCurrentState = MMM.Readers.FullPage.Reader.GetState();
                            }
                            while (lCurrentState == prPreviousState);
                        }
                    }
                    break;
            }
        }
        void ErrorCallbackThreadHelper(MMM.Readers.ErrorCode aErrorCode, string aErrorMessage)
        {
            if (_threadHelperControl.InvokeRequired)
            {
                _threadHelperControl.Invoke(
                    new MMM.Readers.ErrorDelegate(ErrorCallback),
                    new object[] { aErrorCode, aErrorMessage }
                );
            }
            else
            {
                ErrorCallback(aErrorCode, aErrorMessage);
            }
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        void ErrorCallback(MMM.Readers.ErrorCode aErrorCode, string aErrorMessage)
        {

            MessageBox.Show(aErrorMessage);
            System.Windows.Forms.Application.Exit();
        }
        private void guarda_datos()
        {
            // verifico si existe una imagen escaneada
            if (pic_scan.Image != null)
            {

                bool ok = false;
                string codPais = "";



                WinAPI.NoSiempreEncima(this.Handle.ToInt32());
                genera_carpeta();
                try
                {
                    string sql;
                    UConnection DB1 = new UConnection(Properties.Settings.Default.ipBD, Properties.Settings.Default.serverBD, Properties.Settings.Default.usuarioBD, Properties.Settings.Default.passBD);
                    // verifico si no es nulo o vacio para hacer el update
                    foreach (DataGridViewRow row2 in dgvHuespedes.Rows)
                    {

                        if (ToStringNullSafe(row2.Cells["ESCANEAR"].Value) == "Y")
                        {
                            // obtengo la secuencia seleccionada en el grid
                            string secuencia = row2.Cells[1].Value.ToString();
                            if (!String.IsNullOrEmpty(secuencia))
                            {
                                int filasU = 0;
                                sql = string.Format("update freserno set vn_pasaporte = '{0}', vn_edad = {1}, vn_nac_f = '{2}' where vn_reserva = '{3}' and vn_secuencia = {4}",
                                    numPasaporte, edad, fecha_nac, Program.Reserva, secuencia);
                                ok = DB1.EjecutaSQL(sql, ref filasU);
                                DB1.Dispose();
                                // guardo la imagen
                                var image_name = nombreImagen + Program.Reserva + identificadorDoc + secuencia + ".jpg";
                                byte[] imgb = ImageToByte(imagenReal);
                                wsimg.Img_save(carpeta, imgb, image_name);

                                WinAPI.SiempreEncima(this.Handle.ToInt32());
                                MessageBox.Show("SE GUARDARON SATISFACTORIAMENTE LA IMAGEN Y LOS DATOS.", "¡ÉXITO!");
                                pic_scan.Image = null;
                            }
                            llenarFreserno();
                            return;
                        }
                    }

                    int filas = 0;

                    // primero consulto si ya hay un huesped asignado a la reserva
                    sql = "select MAX(VN_SECUENCIA) VN_SECUENCIA from freserno where vn_reserva = '" + Program.Reserva + "' and vn_pasaporte is not null";
                    ++cuentaPasa; // las veces que se escanea un pasaporte
                    try
                    {
                        if (DB1.EjecutaSQL(sql))
                        {
                            string comandoSqls = string.Empty;
                            // si existe, no elimino el primer registro porque ya tiene pasaporte
                            if (DB1.ora_DataReader.HasRows)
                            {
                                while (DB1.ora_DataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(DB1.ora_DataReader["VN_SECUENCIA"]))
                                    {
                                        random = Convert.ToInt16(DB1.ora_DataReader["VN_SECUENCIA"]);
                                    }

                                }
                            }
                            ++random;
                        }
                        // actualizo el codigo de pais en la reserva
                        string comandoSql3 = string.Format("update freserva set rv_pais = '{0}' where rv_reserva = '{1}' and rv_pais is null", codPais, Program.Reserva);
                        ok = DB1.EjecutaSQL(comandoSql3, ref filas);

                        // inserto el nuevo pax que leyo el escaner
                        string comandoSql = string.Format("insert into freserno (vn_reserva,vn_secuencia,vn_apellido,vn_nombre,vn_pase,vn_cupon,vn_edad,vn_edo_civil,vn_tipo_tarjeta,vn_extra_llegada,vn_extra_salida,vn_extra_status,vn_extra_importe,vn_extra_transa,vn_extra_cheque,vn_pasaporte,vn_en_casa,vn_en_casa_f,vn_nac_f) values ('{0}',{1},'{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}',{16},'{17}','{18}')",
                        Program.Reserva, random, apellido, nombre, null, null, edad, null, null, null, null, null, 0.0, null, null, numPasaporte, 0, null, fecha_nac);

                        ok = DB1.EjecutaSQL(comandoSql, ref filas);

                        var image_name = nombreImagen + Program.Reserva + identificadorDoc + random + ".jpg";
                        byte[] imgb = ImageToByte(imagenReal);
                        wsimg.Img_save(carpeta, imgb, image_name);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("ERROR EN EL FRONT: " + ex.Message);
                        // elimino el archivo ya guardado
                        FileInfo file = new FileInfo(testImagePath);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                        pic_scan.Image = null;
                    }
                    finally
                    {
                        DB1.Dispose();
                    }

                    if (filas > 0)
                    {

                        //WinAPI.SiempreEncima(this.Handle.ToInt32());
                        WinAPI.NoSiempreEncima(this.Handle.ToInt32());
                        MessageBox.Show("SE GUARDARON SATISFACTORIAMENTE LA IMAGEN Y LOS DATOS.", "¡ÉXITO!");
                        pic_scan.Image = null;
                    }
                    else
                    {

                        WinAPI.NoSiempreEncima(this.Handle.ToInt32());
                        MessageBox.Show("NO SE GUARDARON LOS DATOS SATISFACTORIAMENTE", "FAVOR DE REVISAR");
                        // elimino el archivo ya guardado
                        FileInfo file = new FileInfo(testImagePath);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                        pic_scan.Image = null;
                    }




                }
                catch (Exception ex)
                {
                }
                llenarFreserno();
            }
        }

        private void guarda_imagen()
        {
            genera_carpeta();
            pic_scan.Image = null;


            // creo un numero aleatorio para que no se repita
            Random randomI = new Random();
            int randomNumber = randomI.Next(0, 100);
            // Obtengo la ruta para la imagen final y la guardo
            var image_name = nombreImagen + Program.Reserva + "DOC" + Convert.ToString(randomNumber) + ".jpg";
            byte[] imgb = ImageToByte(imagenReal);
            wsimg.Img_save(carpeta, imgb, image_name);
            WinAPI.SiempreEncima(this.Handle.ToInt32());
            MessageBox.Show("SE GUARDÓ EL DOCUMENTO SATISFACTORIAMENTE.", "¡ÉXITO!");
        }
        static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            Image bmPhoto = imgPhoto;
            //se llena un array con los codecs de imagenes y se selecciona posicion 1 de JPG
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            ImageCodecInfo jgpEncoder = codecs[1];

            //Encoder para calidad
            System.Drawing.Imaging.Encoder myEncoder =
            System.Drawing.Imaging.Encoder.Quality;

            //Array de parametros para guardar la imagen donde se indica la reduccion de calidad al 10%
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, 50L);
            // se guarda la imagen en memoria y posterior se retorna
            MemoryStream ms = new MemoryStream();
            bmPhoto.Save(ms, jgpEncoder, myEncoderParameters);
            bmPhoto = Image.FromStream(ms);
            return bmPhoto;
        }
        private void genera_carpeta()
        {
            // obtengo la ruta real de las imagenes
            // le quito el año a las siglas del hotel
            inputHotel = "";
            inputHotel = nombreImagen.Substring(0, nombreImagen.Length - 2);
            // consulto la fecha de llegada de la reserva
            UConnection DB = new UConnection(Properties.Settings.Default.ipBD, Properties.Settings.Default.serverBD, Properties.Settings.Default.usuarioBD, Properties.Settings.Default.passBD);
            try
            {
                string comandoSql4 = string.Format("select * from freserva where rv_reserva = '{0}'", Program.Reserva);
                if (DB.EjecutaSQL(comandoSql4))
                {
                    if (DB.ora_DataReader.HasRows)
                    {
                        while (DB.ora_DataReader.Read())
                        {
                            if (!DBNull.Value.Equals(DB.ora_DataReader["RV_LLEGADA"]))
                            {
                                DateTime fec = Convert.ToDateTime(DB.ora_DataReader["RV_LLEGADA"]);
                                string anio = fec.ToString("yyyy");
                                string mes = fec.ToString("MM");
                                string dia = fec.ToString("dd");
                                carpeta = Properties.Settings.Default.serverImagenes + inputHotel + "\\" + anio + "\\" + mes + "\\" + dia + "\\";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SE PUDO OBTENER LA FECHA DE LLEGADA DE LA RESERVACIÓN \n" + ex.Message);
            }
            finally
            {
                DB.Dispose();
            }

            // si no existe el directorio, lo creo
          /*  if (!Directory.Exists(carpeta))
            {
                System.IO.Directory.CreateDirectory(carpeta);
            }*/
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        private string ToStringNullSafe(object value)
        {
            return (value ?? string.Empty).ToString();
        }
        public byte[] ImageToByte(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        /////////////
    }
}
