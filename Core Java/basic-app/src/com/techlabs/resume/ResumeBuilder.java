package com.techlabs.resume;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

public class ResumeBuilder {

	private String personName;
	private String jobTitle;
	private String phoneNo;
	private String yourSelf;
	private String companyTitle;
	private String date;
	private String aboutCompeny;
	private String collegName;
	private String qulification;
	private String[] skills;

	public String getPersonName() {
		return personName;
	}

	public void setPersonName(String personName) {
		this.personName = personName;
	}
	
	public String getJobTitle() {
		return jobTitle;
	}
	
	public void setJobTitle(String jobTitle) {
		this.jobTitle = jobTitle;
	}
	
	public String getPhoneNo() {
		return phoneNo;
	}
	
	public void setPhoneNo(String phoneNo) {
		this.phoneNo = phoneNo;
	}
	
	public String getYourSelf() {
		return yourSelf;
	}
	
	public void setYourSelf(String yourSelf) {
		this.yourSelf = yourSelf;
	}
	
	public String getCompanyTitle() {
		return companyTitle;
	}
	
	public void setCompanyTitle(String companyTitle) {
		this.companyTitle = companyTitle;
	}
	
	public String getDate() {
		return date;
	}
	
	public void setDate(String date) {
		this.date = date;
	}
	
	public String getAboutCompeny() {
		return aboutCompeny;
	}
	
	public void setAboutCompeny(String aboutCompeny) {
		this.aboutCompeny = aboutCompeny;
	}
	
	public String getCollegName() {
		return collegName;
	}
	
	public void setCollegName(String collegName) {
		this.collegName = collegName;
	}
	
	public String getQulification() {
		return qulification;
	}
	
	public void setQulification(String qulification) {
		this.qulification = qulification;
	}
	
	public String[] getSkills() {
		return skills;
	}
	
	public void setSkills(String[] skills) {
		this.skills = skills;
	}
	
	public void buildResume() throws IOException
	{
		BufferedReader bufferedReader=new BufferedReader(new FileReader("src/com/techlabs/resume/data/index.html"));
		String currntLine="";
		String htmlLine="";
		
		while((currntLine = bufferedReader.readLine()) != null)
		{
			htmlLine=htmlLine + currntLine + "\n";
		}
		
		htmlLine=htmlLine.replace("$name",getPersonName());
		htmlLine=htmlLine.replace("$jobTitle",getJobTitle());
		htmlLine=htmlLine.replace("$mobile",getPhoneNo());
		htmlLine=htmlLine.replace("$yourself",getYourSelf());
		htmlLine=htmlLine.replace("$companyName",getCompanyTitle());
		htmlLine=htmlLine.replace("$date",getDate());
		htmlLine=htmlLine.replace("$aboutCompany",getAboutCompeny());
		htmlLine=htmlLine.replace("$collageName",getCollegName());
		htmlLine=htmlLine.replace("$qualification",getQulification());
		for(int i=0;i<skills.length;i++)
		{
			htmlLine=htmlLine.replace("$Skill" + i,skills[i]);
		}
		
		String resumeFileName = getPersonName().replace(" ", "-") + ".html";
		FileWriter writer=new FileWriter("src/com/techlabs/resume/data/"+resumeFileName);
		writer.write(htmlLine);
		writer.close();
		bufferedReader.close();
	}
}
