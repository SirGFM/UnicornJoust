using UnityEngine;

/**
 * Abstract class for defining a powerup
 */
public abstract class Powerup {

	/**
	 * Do the powerup action
	 * 
	 * @param  [ in]caller The calling object
	 */
	abstract public void use(GameObject caller);

}
