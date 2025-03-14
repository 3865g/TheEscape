using System.Collections;
using Enemy;
using Player;
using Services.PauseService;
using UI.Elements;
using UnityEngine;

namespace Logic
{
    public class GameManager : MonoBehaviour
    {

        public Score score;
        
        private GameObject _spawnPoint;
        private GameObject _player;
        private WaveManager _waveManager;
        private ActorUI _actorUI;
        private bool _isPaused;
        private IPauseService _pauseService;
        
        
        public void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void InitSpawner(GameObject spawnPoint, IPauseService pauseService)
        {
            _spawnPoint = spawnPoint;
            _waveManager = _spawnPoint.GetComponent<WaveManager>();
            _waveManager.StartAfterInitPlayer();
            _waveManager.Construct(_pauseService);
            Invoke(nameof(StartScoringCoroutine), 3f);
        }

        public void InitHud(ActorUI actorUI)
        {
            _actorUI = actorUI;
        }

        public void InitPlayer(GameObject player)
        {
            _player = player;
            _player.GetComponent<PlayerController>().KnockbackAction += PlayerKnocked;
            _player.GetComponent<PlayerController>().RecoverAction += PlayerRecovered;
        }


        private void PlayerKnocked()
        {
            _waveManager.RefreshWaves();
            _isPaused = true;
        }

        private void PlayerRecovered()
        {
            _waveManager.ResumeAfterRefresh();
            _isPaused = false;
        }

        private void StartScoringCoroutine()
        {
            _isPaused = false;
            StartCoroutine(AddScoreOverTime());
        }

        public void AddedScore(int bonusScore)
        {
             score.CurrentScore += bonusScore;
             _actorUI.UpdatePointCounter(score.CurrentScore);
        }
        
        IEnumerator AddScoreOverTime()
        {
            float timer = 0f;   
            while (true)
            {
                if (!_isPaused && !_pauseService.IsPaused)
                {
                    
                    timer += Time.deltaTime;
                    if (timer >= 1f)
                    {
                        score.CurrentScore += 1;
                        timer = 0f;
                        if (_actorUI)
                        {
                            _actorUI.UpdatePointCounter(score.CurrentScore);
                        }
                    }
                    
                }
                yield return null; 
            }
        }

        
    }
}