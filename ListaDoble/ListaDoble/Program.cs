using System;

public enum SortDirection
{
    Asc,
    Desc
}

public interface IMiLista
{
    void InsertInOrder(int value);
    int DeleteFirst();
    int DeleteLast();
    bool DeleteValue(int value);
    int GetMiddle();
    void MergeSorted(IMiLista listA, IMiLista listB, SortDirection direction);
}

public class ListaDoble : IMiLista
{
    private class Nodo
    {
        public int Value;
        public Nodo? Anterior;
        public Nodo? Siguiente;

        public Nodo(int value)
        {
            Value = value;
            Anterior = null;
            Siguiente = null;
        }
    }

    private Nodo? cabeza;
    private Nodo? cola;
    private Nodo? medio;
    private int tamaño;

    public ListaDoble()
    {
        cabeza = null;
        cola = null;
        medio = null;
        tamaño = 0;
    }

    public void InsertInOrder(int value)
    {
        Nodo nuevoNodo = new Nodo(value);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            Nodo? nodoActual = cabeza;
            Nodo? nodoAnterior = null;

            while (nodoActual != null && nodoActual.Value < value)
            {
                nodoAnterior = nodoActual;
                nodoActual = nodoActual.Siguiente;
            }

            if (nodoAnterior == null)
            {
                InsertarAlInicio(nuevoNodo);
            }
            else if (nodoActual == null)
            {
                InsertarAlFinal(nuevoNodo);
            }
            else
            {
                InsertarEnMedio(nuevoNodo, nodoAnterior, nodoActual);
            }
        }

        tamaño++;
        ActualizarMedio();
    }

    private void InsertarAlInicio(Nodo nuevoNodo)
    {
        nuevoNodo.Siguiente = cabeza;
        if (cabeza != null) cabeza.Anterior = nuevoNodo;
        cabeza = nuevoNodo;
    }

    private void InsertarAlFinal(Nodo nuevoNodo)
    {
        if (cola != null) cola.Siguiente = nuevoNodo;
        nuevoNodo.Anterior = cola;
        cola = nuevoNodo;
    }

    private void InsertarEnMedio(Nodo nuevoNodo, Nodo nodoAnterior, Nodo nodoActual)
    {
        nodoAnterior.Siguiente = nuevoNodo;
        nuevoNodo.Anterior = nodoAnterior;
        nuevoNodo.Siguiente = nodoActual;
        nodoActual.Anterior = nuevoNodo;
    }

    public int DeleteFirst()
    {
        if (cabeza == null) throw new InvalidOperationException("La lista está vacía.");

        int valorEliminado = cabeza.Value;
        cabeza = cabeza.Siguiente;

        if (cabeza != null)
        {
            cabeza.Anterior = null;
        }
        else
        {
            cola = null;
        }

        tamaño--;
        ActualizarMedio();
        return valorEliminado;
    }

    public int DeleteLast()
    {
        if (cola == null) throw new InvalidOperationException("La lista está vacía.");

        int valorEliminado = cola.Value;
        cola = cola.Anterior;

        if (cola != null)
        {
            cola.Siguiente = null;
        }
        else
        {
            cabeza = null;
        }

        tamaño--;
        ActualizarMedio();
        return valorEliminado;
    }

    public bool DeleteValue(int value)
    {
        if (cabeza == null) return false;

        Nodo? nodoActual = cabeza;

        while (nodoActual != null && nodoActual.Value != value)
        {
            nodoActual = nodoActual.Siguiente;
        }

        if (nodoActual == null) return false;

        if (nodoActual.Anterior != null)
            nodoActual.Anterior.Siguiente = nodoActual.Siguiente;
        else
            cabeza = nodoActual.Siguiente;

        if (nodoActual.Siguiente != null)
            nodoActual.Siguiente.Anterior = nodoActual.Anterior;
        else
            cola = nodoActual.Anterior;

        tamaño--;
        ActualizarMedio();
        return true;
    }

    public int GetMiddle()
    {
        if (medio == null) throw new InvalidOperationException("La lista está vacía.");
        return medio.Value;
    }

    public void MergeSorted(IMiLista listA, IMiLista listB, SortDirection direction)
    {
        if (listA == null || listB == null) throw new ArgumentNullException("Una de las listas es nula.");

        Nodo? actualA = ((ListaDoble)listA).cabeza;
        Nodo? actualB = ((ListaDoble)listB).cabeza;
        ListaDoble listaFusionada = new ListaDoble();

        if (direction == SortDirection.Asc)
            MergeAscendente(actualA, actualB, listaFusionada);
        if (direction == SortDirection.Desc)
            MergeDescendente(actualA, actualB, listaFusionada);

        cabeza = listaFusionada.cabeza;
        cola = listaFusionada.cola;
    }

    private void MergeAscendente(Nodo? actualA, Nodo? actualB, ListaDoble listaFusionada)
    {
        while (actualA != null && actualB != null)
        {
            if (actualA.Value < actualB.Value)
            {
                listaFusionada.InsertInOrder(actualA.Value);
                actualA = actualA.Siguiente;
            }
            else
            {
                listaFusionada.InsertInOrder(actualB.Value);
                actualB = actualB.Siguiente;
            }
        }
        InsertarRestantes(actualA, actualB, listaFusionada);
    }

    private void MergeDescendente(Nodo? actualA, Nodo? actualB, ListaDoble listaFusionada)
    {
        while (actualA != null && actualB != null)
        {
            if (actualA.Value > actualB.Value)
            {
                listaFusionada.InsertarAlInicio(new Nodo(actualA.Value));
                actualA = actualA.Siguiente;
            }
            else
            {
                listaFusionada.InsertarAlInicio(new Nodo(actualB.Value));
                actualB = actualB.Siguiente;
            }
        }

        InsertarRestantes(actualA, actualB, listaFusionada);
    }

    private void InsertarRestantes(Nodo? actualA, Nodo? actualB, ListaDoble listaFusionada)
    {
        while (actualA != null)
        {
            listaFusionada.InsertInOrder(actualA.Value);
            actualA = actualA.Siguiente;
        }

        while (actualB != null)
        {
            listaFusionada.InsertInOrder(actualB.Value);
            actualB = actualB.Siguiente;
        }
    }

    private void ActualizarMedio()
    {
        if (tamaño == 0)
        {
            medio = null;
        }
        else
        {
            medio = cabeza;
            for (int i = 0; i < tamaño / 2; i++)
            {
                medio = medio?.Siguiente;
            }
        }
    }

    public void Invert()
    {
        if (cabeza == null) throw new InvalidOperationException("La lista está vacía.");

        Nodo? actual = cabeza;
        Nodo? temporal = null;

        while (actual != null)
        {
            temporal = actual.Anterior;
            actual.Anterior = actual.Siguiente;
            actual.Siguiente = temporal;
            actual = actual.Anterior;
        }

        if (temporal != null && temporal.Anterior != null)
        {
            cabeza = temporal.Anterior;
        }

        ActualizarMedio();
    }
}
