package structural.adapter.node;

public class Employee {

	private String name;
	private int age;
	private Node<Employee> nextNode;

	public Node<Employee> getNextNode() {
		return nextNode;
	}

	public void setNextNode(Node<Employee> nextNode) {
		this.nextNode = nextNode;
	}

	public Employee(String name, int age) {
		this.name = name;
		this.age = age;
	}

	public String getName() {
		return name;
	}

	public int getAge() {
		return age;
	}

}
