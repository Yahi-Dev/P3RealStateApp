namespace RealStateApp.Core.Application.Helpers
{
    public static class CreateCodeByProperty
    {
        public static string GetNewCode(List<string> listCode)
        {
            Random random = new Random();

            while (true)
            {
                string nuevoCodigo = GenerateCodeAleatory(random);

                if (!listCode.Contains(nuevoCodigo))
                {
                    return nuevoCodigo;
                }
            }
        }

        private static string GenerateCodeAleatory(Random random)
        {
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] codigo = new char[6];

            for (int i = 0; i < 6; i++)
            {
                codigo[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }

            return new string(codigo);
        }
    }
}
