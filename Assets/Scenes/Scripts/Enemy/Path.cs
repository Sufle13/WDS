using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<GameObject> nodes;
    List<PathSegment> segments;

    //подготовка сегментов при запуске
    void Start()
    {
        segments = GetSegments();
    }

    //функция создает сегменты из узлов
    public List<PathSegment> GetSegments()
    {
        List<PathSegment> segments = new List<PathSegment>();
        int i;
        for (i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 src = nodes[i].transform.position;
            Vector3 dst = nodes[i+1].transform.position;
            PathSegment segment = new PathSegment(src, dst);
            segments.Add(segment);
        }
        return segments;
    }

    //первая функция для абстрагирования находит ближайший к агенту сегмент
    //ОТОБРАЖАЕТ СМЕЩЕНИЕ ТОЧКИ ВО ВНУТРЕННЕЕ ПРЕДСТАВЛЕНИЕ
    public float GetParam(Vector3 position, float lastParam)
    {
        float param = 0f;
        PathSegment currentSegment = null;
        float tempParam = 0f;
        foreach (PathSegment ps in segments)
        {
            tempParam += Vector3.Distance(ps.a, ps.b);
            if (lastParam <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }
        if (currentSegment == null)
            return 0f;

        //вычисляет направление движения из текущей позиции
        Vector3 currPos = position - currentSegment.a;
        Vector3 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();

        //находит точку в сегменте с помощью проекции вектора
        Vector3 pointInSegment = Vector3.Project(currPos, segmentDirection);

        //возвращает следующую позицию на линии маршрута
        param = tempParam - Vector3.Distance(currentSegment.a, currentSegment.b);
        param += pointInSegment.magnitude;
        return param;
    }

    //ПРЕОБРАЗУЕТ ВНУТРЕННЕЕ ПРЕДСТАВЛЕНИЕ В ПОЗИЦИЮ В ТРЕХМЕРНОМ ПРОСТРАНСТВЕ
    public Vector3 GetPosition(float param)
    {
        //по текущему метоположению находит соответствующий сегмент
        Vector3 position = Vector3.zero;
        PathSegment currentSegment = null;
        float tempParam = 0f;
        foreach(PathSegment ps in segments)
        {
            tempParam += Vector3.Distance(ps.a, ps.b);
            if (param <= tempParam)
            {
                currentSegment = ps;
                break;
            }
        }
        if (currentSegment == null)
            return Vector3.zero;

        //преобразует параметр в точку пространства и возвращает ее
        Vector3 segmentDirection = currentSegment.b - currentSegment.a;
        segmentDirection.Normalize();
        tempParam -= Vector3.Distance(currentSegment.a, currentSegment.b);
        tempParam = param - tempParam;
        position = currentSegment.a + segmentDirection * tempParam;
        return position;
    }

    //функция визуализирует маршрут
    void OnDrowGizmos()
    {
        Vector3 direction;
        Color tmp = Gizmos.color;
        Gizmos.color = Color.red;
        int i;
        for (i = 0; i < nodes.Count - 1; i++)
        {
            Vector3 scr = nodes[i].transform.position;
            Vector3 dst = nodes[i+1].transform.position;
            direction = dst - scr;
            Gizmos.DrawRay(scr, direction);
        }
        Gizmos.color = tmp;
    }
}
// определяет класс Path, абстрагирующий точки маршрута от конкретных пространственных 
// представлений, а затем реализует модель PathFollower,
// использующую эту абстракцию для следования к точкам в пространстве

//КЛАСС Path ОСУЩЕСТВЛЯЕТ УПРАВЛЕНИЕ ПЕРЕМЕЩЕНИЕМ ПО ЗАДАННОМУ МАРШРУТУ

//АЛГОРИТМ СЛЕДОВАНИЯ ПО МАРШРУТУ ПОЛУЧАЕТ НОВУЮ ПОЗИЦИЮ, УСТАНАВЛИВАЕТ ЦЕЛЬ И ПРИМЕНЯЕТ
//МОДЕЛЬ ПРИСЛЕДОВАНИЯ Seek