import React from 'react';
import {createDrawerNavigator} from '@react-navigation/drawer';
import Experiences from '../components/experiencecomponents/Experiences';
import Qualifications from '../components/qualificationcomponents/Qualifications';
import Documents from '../components/documentcomponents/Documents';
import Personaldetails from '../components/personaldetailcomponents/Personaldetails';
// import Documentsupload from '../components/othercomponents/Documentsupload';
import Privacypolicy from '../components/othercomponents/Privacypolicy';
import Termsandconditions from '../components/othercomponents/Termsandcondistions';
import About from '../components/othercomponents/About';
import Drawercontent from './Drawercontent';
import ApplicationHistory from '../components/applicationhistorycomponents/ApplicationHistory';
import CancelandRefunds from '../components/othercomponents/CancelandRefunds';
import TabNavigation from './TabNavigation';
import Homescreen from '../components/home/Homescreen';
import ComingSoon from '../utilities/ComingSoon';

const Drawer = createDrawerNavigator();

const Drawernavigation = () => {
  return (
    <Drawer.Navigator drawerContent={props => <Drawercontent {...props} />}>
      <Drawer.Screen
        name="HomeScreen"
        options={{headerTitle: 'Home'}}
        component={Homescreen}
      />
      <Drawer.Screen
        name="Jobs"
        options={{headerTitle: 'Jobs'}}
        component={TabNavigation}
      />
      <Drawer.Screen
        name="PanCard"
        options={{headerTitle: 'Pan Card'}}
        component={ComingSoon}
      />
      <Drawer.Screen
        name="CollegeAdmission"
        options={{headerTitle: 'College Admission'}}
        component={ComingSoon}
      />
      <Drawer.Screen
        name="Assignments"
        options={{headerTitle: 'Assignments'}}
        component={ComingSoon}
      />
      <Drawer.Screen
        name="FasalRegistration"
        options={{headerTitle: 'Fasal Registration'}}
        component={ComingSoon}
      />
        <Drawer.Screen
        name="CreateResume"
        options={{headerTitle: 'Create Resume'}}
        component={ComingSoon}
      />
      <Drawer.Screen name="Qualifications" component={Qualifications} />
      <Drawer.Screen name="Documents" component={Documents} />
      <Drawer.Screen name="Experiences" component={Experiences} />
      <Drawer.Screen
        name="PersonalDetails"
        options={{headerTitle: 'Personal Details'}}
        component={Personaldetails}
      />
      {/* <Drawer.Screen name="DocumentsUpload" component={Documentsupload} /> */}
      <Drawer.Screen
        name="Privacypolicy"
        options={{headerTitle: 'Privacy Policy'}}
        component={Privacypolicy}
      />
      <Drawer.Screen
        name="Termsandconditions"
        options={{headerTitle: 'Terms & Conditions'}}
        component={Termsandconditions}
      />
      <Drawer.Screen
        name="CancelandRefund"
        options={{headerTitle: 'Cancel & Refund'}}
        component={CancelandRefunds}
      />
      <Drawer.Screen
        name="About"
        options={{headerTitle: 'About Us'}}
        component={About}
      />
      <Drawer.Screen name="ApplicationHistory" component={ApplicationHistory} />
    </Drawer.Navigator>
  );
};
export default Drawernavigation;
