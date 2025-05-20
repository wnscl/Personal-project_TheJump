
using System;
using System.Diagnostics;

public class Study<T>
    //����Ƽ�ȿ� ��ũ��Ʈ�� (�������̺�� x)
    //���� �̷������� Ȱ�밡��
{
    //���׸� ���θ� ���� ��ũ��Ʈ

    //List<int> studyList_a;
    //List<string> studyList_b;
    //<T> T = type �� ����ȿ� ���� �ֵ��� ������ Ÿ���� ��
    //GetComponent<Player> ���
    //Ÿ�� ��ü�� �Ű�����ó�� ������ ����
    //-> ���׸�
    //���׸� �������
    //- Ÿ�Կ� �������� �ʴ� �Ϲ�ȭ�� �ڵ带 �ۼ��ϵ��� �����ֱ� ������
    //�̸��� ->
    //���׸��� ��Ÿ���� ���߿� ������ �� �ְ� ���ִ� Ʋ���̾�.
    //�׷��� ���� ����� �ڵ带 Ÿ�Ժ��� �ݺ����� �ʾƵ� �ǰ�
    //������ִ� ����. �� ����.
    //�� ��¥ ����ؾ��ϴ� ������ �ڵ� ���뼺�� ��ȭ�ϴ� �Ϳ� �ִ�.

    private T[] _array = new T[1];
    // TŸ���� �迭 ����
    private int _lastIndex = 0;
    //�迭�� �ٷ� �� ����� �ε��� ��

    public int Count => _lastIndex;

    public void ShowInfo()
    {
        foreach (var thing in _array)
        {
            Debug.WriteLine(thing);
        }
    }
    
    public bool Contains(T value)
    //����Ʈ�� ��Ұ� ���ԵǾ��ִ��� ���θ� ��ȯ
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
                    $"[studyList] �ε���{index}�� ����Ʈ ������ ���");
            }
            return _array[index];
        }
        set
        {
            if (index < 0 || index >= _lastIndex)
            {
                throw new Exception(
                    $"[studyList] �ε���{index}�� ����Ʈ ������ ���");
            }
            _array[index] = value;
        }
    }

    public void Add(T value)
        //����Ʈ�� ���ο� ��Ҹ� �߰�
        //����Ʈ�� �߰��� ��� = value
    {
        //�迭�� ũ�Ⱑ count���� �۰ų� ������
        if (_array.Length <= Count)
        {
            //ũ�Ⱑ 2���� ���ο� �迭�� ���� _array�� �Ҵ�
            T[] newArray = new T[Count * 2];
            Array.Copy(sourceArray: _array, destinationArray: newArray, length: Count);
            _array = newArray;
        }


        //�迭�� ��� �߰�(add value)
        _array[_lastIndex] = value;
        _lastIndex++;
        
        
    }

    public void Remove(T valueToRemove)
    //����Ʈ���� ��Ҹ� ����
    //������ ��� = valueToRemove
    {
        //�ش� ��� ���翩�� �Ǵ�
        //������ ����
        if (TryGetValue(valueToRemove, out int index) == false)
        {
            return;
        }
        //������ ��Ұ� ����Ʈ�� ������,
        //�� ��ĭ���� ��ĭ�� ����� ��ġ�� ������
        for (int i = index; i < _lastIndex - 1; i++)
        {
            _array[i] = _array[i + 1];
        }

        //�迭 ������ ��Ҵ� ����Ʈ������ ����
        _array[_lastIndex - 1] = default;

        _lastIndex--;
    }

    private bool TryGetValue(T valueToFind,out int index)
    //�迭�� ã�� ��Ұ� �ִ��� Ȯ���ؼ� �ش� ����� index����
    //�Ѱ��ִ� �Լ�
    //valueToFind ã������ ���
    //index �Ѱ��ִ� ����� �ε�����
    {
        index = -1; //�ʱ�ȭ

        for (int i = 0; i < Count; i++)
        //������ �� �ʿ���� ����ִ� ĭ������ Ž���ϸ� ��
        //�׷��� count
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
