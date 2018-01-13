package creational.factorymethod.test;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import creational.factorymethod.IAutoFactory;
import creational.factorymethod.IAutomobile;

public class TestFactoryMethod {

	public static void main(String[] args) throws InstantiationException,
			IllegalAccessException, ClassNotFoundException {

		String className = createFactory();

		IAutoFactory factory = (IAutoFactory) Class.forName(className)
				.newInstance();

		IAutomobile car = factory.make();
		car.start();
		car.stop();
	}

	public static String createFactory() {
		Properties properties = new Properties();
		InputStream inputStream;

		try {
			inputStream = new FileInputStream("PropertyFile/factory.properties");
			properties.load(inputStream);
			return properties.getProperty("Factory");
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

}
