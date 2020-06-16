using System;

namespace Vanlo {
    /// <summary>
    /// Provides the ability to manage delegated construction of client connections for requests.
    /// </summary>
    public static class ClientManager {
        private static Func<Client> getCurrent;

        internal static Client Build() {
            if (getCurrent == null)
                throw new ClientNotConfigured();
            return getCurrent();
        }

        public static void SetCurrent(string apiKey) {
            SetCurrent(() => new Client(new ClientConfiguration(apiKey)));
        }

        public static void SetCurrent(string apiKey, string apiBase) {
            SetCurrent(() => new Client(new ClientConfiguration(apiKey, apiBase)));
        }

        public static void SetCurrent(Func<Client> getClient) {
            getCurrent = getClient;
        }
    }
}
