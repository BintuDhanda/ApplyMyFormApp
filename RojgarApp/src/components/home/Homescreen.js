import React from 'react';
import {StyleSheet, View} from 'react-native';
import {Button, Card, Title} from 'react-native-paper';
import Colors from '../../settings/Colors';
import {ScrollView} from 'react-native-gesture-handler';
import Loader from '../../utilities/Loader';
const Home = ({navigation}) => {
  return (
    <>
      {/* <Loader loading={loading} waiting={waiting} spinner={true} /> */}
      <ScrollView keyboardShouldPersistTaps="handled">
        <View style={styles.container}>
          <View style={styles.titleContainer}>
            <Title style={styles.title}>Our Services</Title>
          </View>
          <View style={styles.cardContainer}>
            <Card
              style={styles.cardInitial}
              onPress={() => navigation.navigate('Jobs')}>
              <Card.Title
                style={styles.cardTitle}
                title="Jobs"
                titleStyle={{margin: 'auto'}}
                titleNumberOfLines={2}
              />
              <Button
                color={Colors.green}
                onPress={() => navigation.navigate('Jobs')}
                style={styles.btn}
                labelStyle={{color: 'white'}}>
                More Detail
              </Button>
            </Card>
            <Card
              style={styles.cardInitial}
              onPress={() => {
                navigation.navigate('PanCard', {name:'Pan Card'});           
              }}>
              <Card.Title
                style={styles.cardTitle}
                title="Pan Card"
                titleStyle={{margin: 'auto'}}
                titleNumberOfLines={2}
              />
              <Button
                color={Colors.green}
                onPress={() => {
                  navigation.navigate('PanCard', {name:'Pan Card'});
                  
                }}
                style={styles.btn}
                labelStyle={{color: 'white'}}>
                More Detail
              </Button>
            </Card>
            <Card
              style={styles.card}
              onPress={() => {
                navigation.navigate('CollegeAdmission', {name:'College Admission'});
              }}>
              <Card.Title
                style={styles.cardTitle}
                title="College Admission"
                titleStyle={{margin: 'auto'}}
                titleNumberOfLines={2}
              />
              <Button
                color={Colors.green}
                onPress={() => {
                  navigation.navigate('CollegeAdmission', {name:'College Admission'});
                }}
                style={styles.btn}
                labelStyle={{color: 'white'}}>
                More Detail
              </Button>
            </Card>
            <Card
              style={styles.card}
              onPress={() => {
                navigation.navigate('Assignments', {name: 'Assignments'});
              }}>
              <Card.Title
                style={styles.cardTitle}
                title="Solved Assignments"
                titleStyle={{margin: 'auto'}}
                titleNumberOfLines={2}
              />
              <Button
                color={Colors.green}
                onPress={() => {
                  navigation.navigate('Assignments', {name: 'Assignments'});
                }}
                style={styles.btn}
                labelStyle={{color: 'white'}}>
                More Detail
              </Button>
            </Card>
            <Card
              style={styles.card}
              onPress={() => {
                navigation.navigate('FasalRegistration', {name: 'Fasal Registration'});
              }}>
              <Card.Title
                style={styles.cardTitle}
                title="Fasal Registration"
                titleStyle={{margin: 'auto'}}
                titleNumberOfLines={2}
              />
              <Button
                color={Colors.green}
                onPress={() => {
                  navigation.navigate('FasalRegistration', {name: 'Fasal Registration'});
                }}
                style={styles.btn}
                labelStyle={{color: 'white'}}>
                More Detail
              </Button>
            </Card>
            <Card
              style={styles.card}
              onPress={() => {
                navigation.navigate('CreateResume', {name: 'Create Resume'});
              }}>
              <Card.Title
                style={styles.cardTitle}
                title="Create Resume"
                titleStyle={{margin: 'auto'}}
                titleNumberOfLines={2}
              />
              <Button
                color={Colors.green}
                onPress={() => {
                  navigation.navigate('CreateResume', {name: 'Create Resume'});
                }}
                style={styles.btn}
                labelStyle={{color: 'white'}}>
                More Detail
              </Button>
            </Card>
          </View>
        </View>
      </ScrollView>
    </>
  );
};

export default Home;

const styles = StyleSheet.create({
  container: {
    padding: 10,
  },
  titleContainer: {
    padding: 10,
  },
  title: {
    color: Colors.primary,
  },
  cardContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
  },
  cardInitial: {
    width: '45%',
    elevation: 5,
  },
  card: {
    width: '45%',
    elevation: 5,
    marginTop: 20,
  },
  cardTitle: {
    alignItems: 'center',
    justifyContent: 'center',
    height: 100,
    width: '100%',
  },
  btn: {
    marginTop: 'auto',
    backgroundColor: Colors.primary,
    borderTopLeftRadius: 0,
    borderTopRightRadius: 0,
  },
});
