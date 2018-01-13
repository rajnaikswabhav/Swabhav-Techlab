package structural.adapter;

import java.util.Iterator;
import java.util.LinkedList;

@SuppressWarnings("hiding")
public class SwabhavQ<T> implements Iterable<T> {

	private LinkedList<T> list = new LinkedList<T>();

	public void enqueue(T item) {
		list.addLast(item);
		System.out.println(list);
	}

	public LinkedList<T> deque() {
		list.removeFirst();
		return list;
	}

	public int count() {
		return list.size();
	}

	@Override
	public Iterator<T> iterator() {
		return list.iterator();
	}

}
