using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProccessing : MonoBehaviour
{
    private float _intensity = 0.4f;
    private Volume _volume;
    private Vignette _vignette;

    private void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
        _vignette.active = false;
    }

    public void OnDamage()
    {
        StartCoroutine(TakeDamageEffect());
    }

    private IEnumerator TakeDamageEffect()
    {
        _intensity = 0.4f;

        _vignette.active = true;
        _vignette.intensity.Override(_intensity);

        yield return new WaitForSeconds(0.2f);

        while(_intensity > 0)
        {
            _intensity -= 0.01f;

            if(_intensity < 0)
                _intensity = 0;

            _vignette.intensity.Override(_intensity);

            yield return new WaitForSeconds(0.01f);
        }

        _vignette.active = false;
        yield break;
    }
}
