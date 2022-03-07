/************************************************/
/*                                              */
/*     Copyright (c) 2018 - 2021 monitor1394    */
/*     https://github.com/monitor1394           */
/*                                              */
/************************************************/

using System.Collections;
using UnityEngine;
using Commons;

namespace XCharts.Examples
{
    [DisallowMultipleComponent]
    public class RewardChart : MonoBehaviour
    {
        private LineChart chart;
        private Serie serie;
        private int m_DataNum = 8;


        void Start()
        {
            
            chart = gameObject.GetComponent<LineChart>();
            if (chart == null) chart = gameObject.AddComponent<LineChart>();
            chart.title.text = "Reward";
            chart.yAxis0.minMaxType = Axis.AxisMinMaxType.Custom;
            chart.RemoveData();
            serie = chart.AddSerie(SerieType.Line, "Line");
        }

        void Update()
        {   
            chart.AddData(0, CommonFunctions.maxR+50);
        }




    }
}