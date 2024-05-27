using System;
using System.Collections;
using System.Collections.Generic;
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
    public class FadeAnimation
    {
        private bool check_animation;

        public FadeAnimation() { check_animation = false; }
        
        public IEnumerator FadeBoth(Image image = null, float delay = 0f, float speed = 0.01f, float start_visibility = 0f)
        {
            if (image != null)
            {

                while (check_animation) yield return new WaitForSeconds(0.2f);
                check_animation = true;

                bool time_delta = delay == 0;

                FadeImage fade_image = new FadeImage(speed, start_visibility);

                while (fade_image.GetVisibility() < 1f)
                {
                    yield return new WaitForSeconds(time_delta ? Time.deltaTime : delay);
                    image.color = fade_image.GetColor();
                    fade_image.In();
                }

                yield return new WaitForSeconds(delay + 0.1f);

                while (fade_image.GetVisibility() > 0f)
                {
                    fade_image.Out();
                    image.color = fade_image.GetColor();
                    yield return new WaitForSeconds(time_delta ? Time.deltaTime : delay);
                }

                check_animation = false;
            }
        }

        public IEnumerator FadeIn(Image image = null, float delay = 0f, float speed = 0.01f, float start_visibility = 0f)
        {
            if (image != null) {
                while (check_animation) yield return new WaitForSeconds(0.2f);
                check_animation = true;
                bool time_delta = delay == 0;

                FadeImage fade_image = new FadeImage(speed, start_visibility);
                while (fade_image.GetVisibility() < 1f)
                {

                    yield return new WaitForSeconds(time_delta ? Time.deltaTime : delay);
                    image.color = fade_image.GetColor();
                    fade_image.In();
                }

                check_animation = false;
            }
        }

        public IEnumerator FadeOut(Image image = null, float delay = 0f, float speed = 0.01f, float start_visibility = 0f)
        {
            if (image != null)
            {
                while (check_animation) yield return new WaitForSeconds(0.2f);
                bool time_delta = delay == 0;
                check_animation = true;


                FadeImage fade_image = new FadeImage(speed, start_visibility);

                while (fade_image.GetVisibility() < 1f)
                {
                    yield return new WaitForSeconds(time_delta ? Time.deltaTime : delay);
                    image.color = fade_image.GetColor();
                    fade_image.Out();
                }

                check_animation = false;
            }
        }

        public IEnumerator FadeInChildren(GameObject game_object, float delay = 0f,
                                            float speed = 0.01f, float start_visibility = 0f)
        {
            if (game_object != null)
            {
                List<Image> images = new List<Image>();

                foreach (Image child in game_object.GetComponentsInChildren<Image>()) images.Add(child);
                
                
            }


        }
    }
}