using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XCharts.Examples
{   
    [DisallowMultipleComponent]
    public class TriggerDistribution : MonoBehaviour
    {
            private BarChart chart;
            private Serie serie, serie2;
            private int m_DataNum = 5;
            void Start()
            {
                
                chart = gameObject.GetComponent<BarChart>();
                if (chart == null) chart = gameObject.AddComponent<BarChart>();
                chart.title.text = "Trigger Source";
                chart.title.subText = "普通柱状图";

                chart.yAxis0.minMaxType = Axis.AxisMinMaxType.Default;

                chart.RemoveData();
                serie = chart.AddSerie(SerieType.Bar, "Bar1");
                serie.label.show = true;
                serie2 = chart.AddSerie(SerieType.Bar, "Bar2");
                serie2.label.show = true;
                serie2.lineType = LineType.Normal;
                for (int i = 0; i < 1; i++)
                {
                    chart.AddData(0, 0);
                    chart.AddData(1, 0);
                }

            }

            void Update()
            {   
                for (int i = 0; i < 1; i++)
                {
                    chart.UpdateData(0, 0,UnityEngine.Random.Range(30, 90));
                    chart.UpdateData(1, 0,UnityEngine.Random.Range(30, 90));
                }
            
            }
            
        
    }
}
