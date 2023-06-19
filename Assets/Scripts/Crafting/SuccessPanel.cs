using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Asyncoroutine;

namespace Crafting
{
    public class SuccessPanel : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI statsText;

        [SerializeField] private float timeShown = 1.5f;

        [SerializeField] private float upperLimit;

        [SerializeField] private float speed = 100;

        public async void Show(Sprite img, string itemName, string stats)
        {
            var initialPos = transform.position;
            
            icon.sprite = img;
            nameText.text = itemName;
            statsText.text = stats;
            
            gameObject.SetActive(true);

            while (!Mathf.Approximately(transform.position.y, initialPos.y + upperLimit))
            {
                await new WaitForEndOfFrame();

                transform.position = Vector3.MoveTowards(transform.position,
                    initialPos + Vector3.up * upperLimit,
                    speed * Time.deltaTime
                );
            }

            await new WaitForSeconds(timeShown);

            while (!Mathf.Approximately(transform.position.y, initialPos.y))
            {
                await new WaitForEndOfFrame();
                transform.position = Vector3.MoveTowards(transform.position,
                    initialPos,
                    speed * Time.deltaTime);
            }
            
            gameObject.SetActive(false);
        }
    }
}
