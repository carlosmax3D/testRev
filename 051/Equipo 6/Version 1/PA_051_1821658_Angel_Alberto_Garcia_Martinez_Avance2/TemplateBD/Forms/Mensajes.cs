namespace TemplateCSharp.Forms;

// Cuadros de mensaje con los títulos e iconos que usa el proyecto
public static class Mensajes
{
    public static void Aviso(string texto)
    {
        MessageBox.Show(texto, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void Informacion(string texto)
    {
        MessageBox.Show(texto, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static bool Confirmar(string pregunta)
    {
        return MessageBox.Show(pregunta, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}
