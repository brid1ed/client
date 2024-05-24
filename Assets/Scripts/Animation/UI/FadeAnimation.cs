using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Animation.UI {

    class FadeImage {
        
        private float speed;
        private float visibility;
        private Color color;

        public FadeImage(Color color, float speed, float visibility) {
            this.color = color;
            this.speed = speed;
            this.visibility = visibility;
        }

        public FadeImage(float speed, float visibility):this(new Color(255,255,255, 0), speed, visibility) { }

        public FadeImage(float speed) : this(new Color(255, 255, 255, 0), speed, 0) { }

        public Color GetColor() {
            color.a = GetVisibility();
            return color;
        }
        
        public float GetVisibility() { return this.visibility; }

        public float In() {
            this.visibility += this.speed;
            return GetVisibility();
        }

        public float Out() {
            this.visibility -= this.speed;
            return GetVisibility();
        }
        
    }
    public class FadeAnimation {
        
        public IEnumerator FadeBoth(float delay, float speed,
                                    Image image, float start_visibility = 0f) {
            FadeImage fade_image = new FadeImage(speed, start_visibility);
            
            while (fade_image.GetVisibility() < 1f) {
                yield return new WaitForSeconds(delay);
                image.color = fade_image.GetColor();
                fade_image.In();
            }

            yield return new WaitForSeconds(delay+0.1f);
            
            while (fade_image.GetVisibility() > 0f) {
                fade_image.Out();
                image.color = fade_image.GetColor();
                yield return new WaitForSeconds(delay);
            }
            
        }

        public IEnumerator FadeIn(float delay, float speed,
                                    Image image, float start_visibility = 0f) {
            FadeImage fade_image = new FadeImage(speed, start_visibility);
            while (fade_image.GetVisibility() < 1f) {
                
                yield return new WaitForSeconds(delay);
                image.color = fade_image.GetColor();
                fade_image.In();
            }
        }

        public IEnumerator FadeOut(float delay, float speed,
                                    Image image,float start_visibility = 0f)
        {
            FadeImage fade_image = new FadeImage(speed,start_visibility);
            
            while (fade_image.GetVisibility() < 1f) {
                yield return new WaitForSeconds(delay);
                image.color = fade_image.GetColor();
                fade_image.Out();
            }
        }
        
    }
}