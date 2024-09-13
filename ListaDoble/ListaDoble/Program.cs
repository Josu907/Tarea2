using System;
using System.ComponentModel.Design;

// Crear el enum.
public enum SortDirection
{
    Asc,  // Ascendente
    Desc  // Descendente
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
    // Definir nodos
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
        //Cambiar todo
        Nodo nuevoNodo = new Nodo(value);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            Nodo? actual = cabeza;
            Nodo? anterior = null;

            while (actual != null && actual.Value < value)
            {
                anterior = actual;
                actual = actual.Siguiente;
            }

            if (anterior == null) // Insertar al inicio
            {
                nuevoNodo.Siguiente = cabeza;
                cabeza.Anterior = nuevoNodo;
                cabeza = nuevoNodo;
            }
            else if (actual == null) // Insertar al final
            {
                anterior.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = anterior;
                cola = nuevoNodo;
            }
            else // Insertar en el medio
            {
                anterior.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = anterior;
                nuevoNodo.Siguiente = actual;
                actual.Anterior = nuevoNodo;
            }
        }

        tamaño++;
        ActualizarMedio();
    }

    public int DeleteFirst()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return -1;
        }

        int valorEliminado = cabeza.Value;
        cabeza = cabeza.Siguiente;

        if (cabeza != null)
        {
            cabeza.Anterior = null;
        }
        else
        {
            cola = null; // Lista vacía
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
            cabeza = null; // Lista vacía
        }

        tamaño--;
        ActualizarMedio();
        return valorEliminado;
    }

    public bool DeleteValue(int value)
    {
        if (cabeza == null) return false;

        Nodo? actual = cabeza;

        while (actual != null && actual.Value != value)
        {
            actual = actual.Siguiente;
        }

        if (actual == null) return false; // No se encontró el valor

        if (actual.Anterior != null)
            actual.Anterior.Siguiente = actual.Siguiente;
        else
            cabeza = actual.Siguiente; // Eliminación de la cabeza

        if (actual.Siguiente != null)
            actual.Siguiente.Anterior = actual.Anterior;
        else
            cola = actual.Anterior; // Eliminación de la cola

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
        // Implementación de MergeSorted (requiere recorrer ambas listas y fusionarlas en orden)
    }

    private void ActualizarMedio()
    {
        if (tamaño == 0)
        {
            medio = null;
        }
        else if (tamaño == 1)
        {
            medio = cabeza;
        }
        else
        {
            Nodo? temp = cabeza;
            for (int i = 0; i < tamaño / 2; i++)
            {
                temp = temp.Siguiente;
            }
            medio = temp;
        }
    }
}
