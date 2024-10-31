using System;
using System.Threading.Tasks;
using Windows.Storage;
using WeatherApp.Domain.Common;

namespace WeatherApp.Infrastructure.Common
{
    public class PathService : IPathService
    {
        private bool _initialized;
        private StorageFolder _settingsFolder;

        public async Task<string> InitializeAsync()
        {
            if (_initialized)
                throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");

            _initialized = true;

            const string settingsFolderName = "Settings";
            _settingsFolder =
                await ApplicationData.Current.LocalFolder.CreateFolderAsync(settingsFolderName,
                    CreationCollisionOption.OpenIfExists);

            var settingsFile = await _settingsFolder.TryGetItemAsync("Settings.json") as StorageFile;
            if (settingsFile == null)
                return "";

            var serializedSettings = await FileIO.ReadTextAsync(settingsFile);

            return serializedSettings;
        }

        public async Task SaveAsync(string settings)
        {
            EnsureInitialized();
            var file = await _settingsFolder.CreateFileAsync("Settings.json", CreationCollisionOption.OpenIfExists);

            await FileIO.WriteTextAsync(file, settings);
        }

        private void EnsureInitialized()
        {
            if (!_initialized)
                throw new InvalidOperationException($"{nameof(IPathService)} is not initialized");
        }
    }
}