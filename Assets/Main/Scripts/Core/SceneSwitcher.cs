using System.Collections;
using System.Threading.Tasks;
using Game.Core.DatabaseSystem;
using Game.Generated;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UTIRLib;
using Zenject;

#nullable enable
namespace Game.Core
{
    public class SceneSwitcher : MonoX
    {
        private AssetDatabaseRegistry assetDatabaseRegistry = null!;

        protected override void OnStart()
        {
            base.OnStart();
            assetDatabaseRegistry = ProjectContext.Instance.Container.Resolve<AssetDatabaseRegistry>();
            StartCoroutine(Switch());
        }

        private IEnumerator Switch()
        {
            Task<SceneInstance> task = assetDatabaseRegistry.Scenes.LoadSceneAsync(AssetDatabaseNames.SCENES, "GameplayTest");
            yield return new WaitUntil(() => task.IsCompleted);
            SceneManager.SetActiveScene(task.Result.Scene);
            Destroy(gameObject);
        }
    }
}
