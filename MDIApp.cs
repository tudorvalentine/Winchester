using System;
using System.Windows.Forms;
// declarăm clasa principală a aplicației
class MDIApp {
    // funcția Main definește punctul de intrare al aplicației
    [STAThread]
    public static void Main() {
        // creăm fereastra principală a aplicației
        MainMDIWnd MainWnd = new MainMDIWnd();
        // metoda Run lansează ciclul de prelucrare a mesajelor și vizualizează fereastra pe ecran
        Application.Run(MainWnd);
    }
}
