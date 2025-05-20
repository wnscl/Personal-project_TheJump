
using System;
using System.Diagnostics;

public class Study<T>
    //유니티안에 스크립트만 (모노비헤이비어 x)
    //만들어서 이런식으로 활용가능
{
    //제네릭 공부를 위한 스크립트

    //List<int> studyList_a;
    //List<string> studyList_b;
    //<T> T = type 즉 꺽쇠안에 들어가는 애들은 변수의 타입이 들어감
    //GetComponent<Player> 등등
    //타입 자체를 매개변수처럼 전달이 가능
    //-> 제네릭
    //제네릭 사용이유
    //- 타입에 의존하지 않는 일반화된 코드를 작성하도록 도와주기 때문에
    //이말은 ->
    //제네릭은 “타입을 나중에 지정할 수 있게 해주는 틀”이야.
    //그래서 같은 기능의 코드를 타입별로 반복하지 않아도 되게
    //만들어주는 거지. 와 같다.
    //즉 진짜 사용해야하는 이유는 코드 재사용성을 강화하는 것에 있다.

    private T[] _array = new T[1];
    // T타입의 배열 선언
    private int _lastIndex = 0;
    //배열을 다룰 때 사용할 인덱스 값

    public int Count => _lastIndex;

    public void ShowInfo()
    {
        foreach (var thing in _array)
        {
            Debug.WriteLine(thing);
        }
    }
    
    public bool Contains(T value)
    //리스트에 요소가 포함되어있는지 여부를 반환
    {
        return TryGetValue(value, out int _);
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _lastIndex)
            {
                throw new Exception(
                    $"[studyList] 인덱스{index}가 리스트 범위를 벗어남");
            }
            return _array[index];
        }
        set
        {
            if (index < 0 || index >= _lastIndex)
            {
                throw new Exception(
                    $"[studyList] 인덱스{index}가 리스트 범위를 벗어남");
            }
            _array[index] = value;
        }
    }

    public void Add(T value)
        //리스트에 새로운 요소를 추가
        //리스트에 추가할 요소 = value
    {
        //배열의 크기가 count보다 작거나 같으면
        if (_array.Length <= Count)
        {
            //크기가 2배인 새로운 배열을 만들어서 _array에 할당
            T[] newArray = new T[Count * 2];
            Array.Copy(sourceArray: _array, destinationArray: newArray, length: Count);
            _array = newArray;
        }


        //배열에 요소 추가(add value)
        _array[_lastIndex] = value;
        _lastIndex++;
        
        
    }

    public void Remove(T valueToRemove)
    //리스트에서 요소를 삭제
    //삭제할 요소 = valueToRemove
    {
        //해당 요소 존재여부 판단
        //없으면 종료
        if (TryGetValue(valueToRemove, out int index) == false)
        {
            return;
        }
        //삭제할 요소가 리스트에 있으면,
        //그 뒷칸부터 한칸씩 요소의 위치를 땡겨줌
        for (int i = index; i < _lastIndex - 1; i++)
        {
            _array[i] = _array[i + 1];
        }

        //배열 마지막 요소는 디폴트값으로 수정
        _array[_lastIndex - 1] = default;

        _lastIndex--;
    }

    private bool TryGetValue(T valueToFind,out int index)
    //배열에 찾는 요소가 있는지 확인해서 해당 요소의 index값을
    //넘겨주는 함수
    //valueToFind 찾으려는 요소
    //index 넘겨주는 요소의 인덱스값
    {
        index = -1; //초기화

        for (int i = 0; i < Count; i++)
        //끝까지 갈 필요없이 들어있는 칸까지만 탐색하면 됨
        //그래서 count
        {
            T savedValue = _array[i];
            if (savedValue.Equals(valueToFind))
            {
                index = i;
                return true;
            }
        }

        return false;
    }
}
