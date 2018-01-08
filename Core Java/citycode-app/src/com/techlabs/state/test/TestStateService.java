package com.techlabs.state.test;

import com.techlabs.states.StateService;

public class TestStateService {

	public static void main(String[] args) {
		
		StateService stateService=new StateService();
		stateService.inIt();
		System.out.println(stateService.search("GJ"));
	}

}
