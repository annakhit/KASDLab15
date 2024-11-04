using System;

public class MyArrayDeque<T>
{
    private T[] elements;
    private int head = 0;
    private int tail = 0;

    public MyArrayDeque()
    {
        elements = new T[16];
    }

    public MyArrayDeque(int numElements)
    {
        elements = new T[numElements];
    }
    public MyArrayDeque(T[] a)
    {
        elements = (T[])a.Clone();
        tail = a.Length;
    }

    public void Add(T element) // AddLast()
    {
        if (elements.Length == tail) UpdateCapacity();
        elements[tail] = element;
        tail++;
    }

    public void AddAll(params T[] array)
    {
        foreach (T element in array) Add(element);
    }

    public void Clear()
    {
        elements = new T[elements.Length];
        head = 0;
        tail = 0;
    }

    public bool Contains(object obj)
    {
        foreach (T element in elements)
        {
            if (element == null) return false;
            if (obj.Equals(element)) return true;
        }
        return false;
    }

    public bool ContainsAll(params T[] array)
    {
        foreach (T element in array)
        {
            if (!Contains(element)) return false;
        }
        return true;
    }

    public bool IsEmpty() => head == tail;

    public void Remove(object obj)
    {
        while (Contains(obj)) RemoveFirstOccurrence(obj);
    }

    public void RemoveAll(params T[] array)
    {
        foreach (T element in array) Remove(element);
    }

    public void RetainAll(params T[] array)
    {
        foreach (T element in array)
        {
            if (!Contains(element)) Remove(element);
        }
    }

    public int Size() => tail - head;
    public T[] ToArray()
    {
        T[] result = new T[elements.Length];
        for (int index = 0; index < elements.Length; index++)
        {
            result[index] = elements[index];
        }
        return result;
    }

    public T[] ToArray(ref T[] array)
    {
        if (array == null) ToArray();

        if (array.Length < elements.Length)
            throw new ArgumentOutOfRangeException();

        for (int index = 0; index < elements.Length; index++)
        {
            array[index] = elements[index];
        }
        return array;
    }
    public T Peek() // Element () GetFirst() PeekFirst()
    {
        if (Size() == 0) return default;
        return elements[head];
    }
    public T Pull() // Pop() PollFirst() RemoveFirst()
    {
        if (Size() == 0) return default;
        T element = elements[head];
        elements[0] = default;
        head++;
        return element;
    }
    public void AddFirst(T obj) // Push()
    {
        if (head > 0)
        {
            elements[--head] = obj;
            return;
        }

        if (elements.Length == tail) UpdateCapacity();
        for (int i = tail; i > 0; i--) elements[i] = elements[i - 1];
        tail++;
        elements[0] = obj;
    }
    public T GetLast() => tail > 0 ? elements[tail - 1] : default; // PeekLast()

    public bool OfferFirst(T obj)
    {
        if (tail == elements.Length && head == 0) return false;
        AddFirst(obj);
        return true;
    }

    public bool OfferLast(T obj)
    {
        if (tail == elements.Length) return false;
        Add(obj);
        return true;
    }
    public T PullLast() // RemoveLast()
    {
        if (tail == 0) return default;
        T result = elements[tail - 1];
        elements[tail - 1] = default;
        tail--;
        return result;
    }

    public bool RemoveLastOccurrence(object obj)
    {
        int position = -1;
        int size = elements.Length;
        T[] newElements = new T[size];

        for (int index = size - 1; index >= 0; index--)
        {
            if (obj.Equals(elements[index]))
            {
                position = index;
                break;
            }
            newElements[index] = elements[index];
        }

        for (int index = position; index > 0; index--)
        {
            newElements[index] = elements[index - 1];
        }

        head = position == -1 ? head : head + 1;
        elements = newElements;

        return position != -1;
    }
    public bool RemoveFirstOccurrence(object obj)
    {
        int position = -1;
        int size = elements.Length;
        T[] newElements = new T[size];

        for (int index = 0; index < size; index++)
        {
            if (obj.Equals(elements[index]))
            {
                position = index;
                break;
            }
            newElements[index] = elements[index];
        }

        for (int index = position; index < size - 1; index++)
        {
            newElements[index] = elements[index + 1];
        }

        tail = position == -1 ? tail : tail - 1;
        elements = newElements;

        return position != -1;
    }
    private void UpdateCapacity()
    {
        int size = Size();
        T[] newArray = new T[size * 2];
        for (int i = 0; i < size; i++) newArray[i] = elements[i];
        elements = newArray;
    }

    public void Print()
    {
        foreach (T element in elements)
        {
            if (element != null)
                Console.WriteLine($"{element} ");
        }
    }
}
