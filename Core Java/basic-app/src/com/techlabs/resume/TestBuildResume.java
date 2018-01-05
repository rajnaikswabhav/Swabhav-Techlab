package com.techlabs.resume;

import java.io.IOException;

public class TestBuildResume {

	public static void main(String[] args) throws IOException {

		ResumeBuilder builder = new ResumeBuilder();
		builder.setPersonName("Akash R. Malaviya");
		builder.setPhoneNo("7405679727");
		builder.setJobTitle("Java Developer");
		builder.setYourSelf("Hi there \n"
				+ "My name is Akash Malaviya.I am Computer Engineer.");
		builder.setCompanyTitle("Vakrtund Infotech");
		builder.setDate("14th November 2017");
		builder.setAboutCompeny("Vakrtund Infotech is very good company.");
		builder.setCollegName("Ahmedabad Institue of Technology");
		builder.setQulification("BE-Computer Engineer");
		builder.setSkills(new String[]{"Coding","Quick Learner","Good Understanding","good speed"});
		builder.buildResume();

	}

}
