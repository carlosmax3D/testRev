namespace TemplateCSharp.Forms;

// Mapa del estadio: lo convierte en imagen y lo muestra en una ventana aparte
public static class VisorMapa
{
    // Se copia la imagen para que no dependa del flujo de memoria, que se cierra al terminar
    public static Image CrearImagen(byte[] mapa)
    {
        using MemoryStream flujo = new(mapa);
        using Image original = Image.FromStream(flujo);
        return new Bitmap(original);
    }

    public static void Mostrar(IWin32Window dueno, byte[] mapa, string titulo)
    {
        using Image imagen = CrearImagen(mapa);
        Mostrar(dueno, imagen, titulo);
    }

    // No libera la imagen: la conserva quien la mandó
    public static void Mostrar(IWin32Window dueno, Image imagen, string titulo)
    {
        using Form visor = new()
        {
            Text = titulo,
            ClientSize = new Size(800, 600),
            StartPosition = FormStartPosition.CenterParent,
            MinimizeBox = false,
            ShowIcon = false,
            ShowInTaskbar = false
        };
        visor.Controls.Add(new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = imagen
        });
        visor.ShowDialog(dueno);
    }
}
