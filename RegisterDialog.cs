using System;
using System.Windows.Forms;
using System.Drawing;

using WinchesterDE;

// declarăm o clasă pentru caseta de dialog 
class RegisterDialog : Form {
    private Label question_Label;
    private Button m_OkButton;
    private Button m_CancelButton;

    // constructorul casetei de dialog public 
    public RegisterDialog() {
        // stabilim tipul marginii casetei de dialog   
        FormBorderStyle = FormBorderStyle.FixedDialog;   
        // stabilim dimensiunea casetei de dialog   
        Size = new System.Drawing.Size(230,140);   
        // arătăm că caseta de dialog va fi centrată referitor la fereastra părinte   
        StartPosition = FormStartPosition.CenterParent;
        // dezactivăm meniul de sistem 
        ControlBox = false;   
        // dezactivăm butoanele de minimizare şi de maximizare a ferestrei   
        MinimizeBox = false;  
        MaximizeBox = false;   
        // ascundem fereastra pe bara de tascuri ShowInTaskbar = false; stabilim textul în antetul casetei de dialog   
        Text="Alert!";

        question_Label = new Label(); 
        question_Label.Text = "Do you want to save the changes?"; 
        question_Label.TextAlign = ContentAlignment.MiddleCenter; 
        question_Label.Size = new System.Drawing.Size(180, 50); 
        question_Label.Location = new System.Drawing.Point(10, 10); 
        Controls.Add(question_Label);

        m_OkButton = new Button(); 
        m_OkButton.Size = new System.Drawing.Size(80, 24); 
        m_OkButton.Location = new System.Drawing.Point(120, 70);
        m_OkButton.Text = "Yes"; 
        m_OkButton.Click += new System.EventHandler(OkButton_Click);
        // dacă vrem să eliminam Handler-ul decomentăm acest rând
        // m_OkButton.DialogResult = DialogResult.Yes;
        Controls.Add(m_OkButton);

        m_CancelButton = new Button(); 
        m_CancelButton.Size = new System.Drawing.Size(80, 24); 
        m_CancelButton.Location = new System.Drawing.Point(10, 70); 
        m_CancelButton.Text = "No"; 
        m_CancelButton.Click += new System.EventHandler(CancelButton_Click);
        // dacă vrem să eliminăm Handler-ul decomentăm acest rând
        // m_CancelButton.DialogResult=DialogResult.Cancel; 
        Controls.Add(m_CancelButton);

        // pentru ca cu Enter să se acceseze OK și pentru ca Esc să fie egal cu Cancel
        AcceptButton = m_OkButton;
        CancelButton = m_CancelButton;
    }

    // HANDLERE
    // utilizatorul nu poate închide prin intermediul butonului OK dacă există erori de validare
    private void OkButton_Click(object sender, System.EventArgs e) { 
        DialogResult = DialogResult.Yes;
    }

    private void CancelButton_Click(object sender, System.EventArgs e) { 
        DialogResult = DialogResult.Cancel;
    }
}
