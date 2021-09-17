/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
	public Image ImgFoodBar;
	public Text TxFood;
	public int Min;
	public int Max;
	private int mCurrentValue;
	private float mCurrentPercent;
	
	public void SetFood(int food)
	{
		if(health != mCurrentPercent)
		{
			if(Max - Min == 0)
			{
				mCurrentPercent = 0;
				mCurrentValue = 0;
			}
			else
			{
				mCurrentValue = food;
				mCurrentPercent = mCurrentValue/(Max-Min);
				
			}
			TxFood.text = string.Format("{0}%", Mathf.RoundToInt(mCurrentPercent * 100));
			ImgFoodBar.fillAmount = mCurrentPercent;
		}
	}
	
	public float CurrentPercent
	{
		get{ return mCurrentPercent; }
	}
	
	public float CurrentValue
	{
		get{ return mCurrentValue; }
	}
	
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
