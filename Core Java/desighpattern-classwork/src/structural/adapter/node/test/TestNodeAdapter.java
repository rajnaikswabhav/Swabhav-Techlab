package structural.adapter.node.test;

import structural.adapter.node.Employee;
import structural.adapter.node.Node;

public class TestNodeAdapter {

	public static void main(String[] args) {
		
		Node<Employee> node1 = new Node<Employee>();
		Node<Employee> node2 = new Node<Employee>();
		Node<Employee> node3 = new Node<Employee>();
		
		Employee employee1 = new Employee("Akash", 21);
		Employee employee2 = new Employee("Brijesh", 21);
		Employee employee3 = new Employee("Parth", 21);
		
		node1.setValue(employee1);
		node1.setNextNode(node2);
		
		node2.setValue(employee2);
		node2.setNextNode(node3);
		
		node3.setValue(employee3);
		node3.setNextNode(null);
		
		print(node1);
		
	}

	public static void print(Node node) {

		while(true){
			if(node == null)  break;
			
			Employee employee = (Employee) node.getValue();
			System.out.println("Employee Name: "+employee.getName());
			System.out.println("Employee Age: "+employee.getAge()+ "\n");
			node= node.getNextNode();
		}
	}
}
