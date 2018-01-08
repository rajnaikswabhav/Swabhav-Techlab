package com.techlabs.state.test;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import com.techlabs.states.StateService;

public class TestStateService {

	public static void main(String[] args) {

		StateService stateService = new StateService();
		Map<String, String> stateList = new HashMap<String, String>();
		stateService.inIt();
		stateList = stateService.search("kera");
		Iterator itr = stateList.entrySet().iterator();
		while (itr.hasNext()) {
			System.out.println(itr.next());
		}

	}

}
