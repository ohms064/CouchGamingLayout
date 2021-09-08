using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
public class EmojiParticles : CharacterChatListener {
    [SerializeField, Required]
    private ParticleSystem _particles;
    [SerializeField, Required]
    private ParticleSystemRenderer _renderer;
    [SerializeField]
    private float _burstTime = 0.3f;

    private TwitchEmoteManager _manager;

    public void OnSpawn ( TwitchEmoteManager manager ) {
        _manager = manager;
    }

    public override bool CheckMessage ( CharacterChatMessage message ) {
        return message.HasEmotes;
    }

    public override void ManageMessage ( CharacterChatMessage message ) {
        _manager.GetEmotes( from c in message.Emotes select c.ImageUrl, ( textures ) => StartCoroutine( ShowParticles( textures ) ) );
    }

    private IEnumerator ShowParticles ( IEnumerable<Texture2D> textures ) {
        foreach ( var texture in textures ) {
            ShowParticle( texture );
            yield return new WaitForSeconds( _burstTime );
        }
    }

    private void ShowParticle ( Texture2D texture ) {
        _renderer.material.SetTexture( "_BaseMap", texture );
        _particles.Play();
    }
}
