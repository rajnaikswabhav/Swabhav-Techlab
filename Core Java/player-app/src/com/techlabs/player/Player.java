package com.techlabs.player;

public class Player {

	private int id;
	private String name;
	private int age;

	public Player(int id, String name) {
		this(id, name, 18);
	}

	public Player(int id, String name, int age) {
		this.id = id;
		this.name = name;
		this.age = age;
	}

	public int getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public int getAge() {
		return age;
	}

	public void setAge(int age) {
		this.age = age;
	}

	public Player whoIsElder(Player player) {
		if (this.age > player.age) {
			return this;
		}
		return player;
	}

	@Override
	public boolean equals(Object obj)
	{
		if(this.id==((Player) obj).getId())
		{
			return true;
		}
		else{
			return false;
		}
	}

	@Override
	public String toString() {
		return "Id: " + id + "\n" + "Name: " + name + "\n" + "Age: " + age
				+ "\n" +super.toString() +"\n";
	}
}
